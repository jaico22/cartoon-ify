using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Core.ImageProcessing
{
    public class ImageProcessor
    {
        private Mat _imageMat; 
        public async Task<Stream> CartoonifyImage(Stream imageStream)
        {
            await StreamToMat(imageStream);

            _imageMat = ReduceColorResolution(_imageMat);

            DrawBorders();

            return MatToStream();
        }

        private void DrawBorders()
        {
            var furtherReducedImage = new Mat();

            Cv2.EdgePreservingFilter(_imageMat, _imageMat);

            furtherReducedImage = ReduceColorResolution(_imageMat, bitShift: 2, scaleFactor: 1);

            var grayImage = new Mat();
            Cv2.CvtColor(furtherReducedImage, grayImage, ColorConversionCodes.BGR2GRAY);

            var GradX = new Mat();
            Cv2.Sobel(grayImage, GradX, xorder: 1, yorder: 0, ddepth: MatType.CV_32F);
            var GradY = new Mat();
            Cv2.Sobel(grayImage, GradY, xorder: 0, yorder: 1, ddepth: MatType.CV_32F);

            var gradient = new Mat();
            Cv2.Subtract(GradX, GradY, gradient);
            Cv2.ConvertScaleAbs(gradient, gradient);

            Cv2.GaussianBlur(gradient, gradient, new Size(7, 7), 2.5);

            var thresholding = new Mat();
            Cv2.Threshold(gradient, thresholding, 100, 255, ThresholdTypes.Binary);

            var borderMat = new Mat(_imageMat.Size(), _imageMat.Type());

            Cv2.BitwiseAnd(_imageMat, borderMat, _imageMat, mask: thresholding);


        }

        private Mat ReduceColorResolution(Mat inputImage, int bitShift = 4, int scaleFactor = 2)
        {
            var newImage = new Mat(inputImage.Size(), inputImage.Type());
            for (int j = 1; j < inputImage.Rows; j++)
            {
                for (int i = 1; i < inputImage.Cols; i++)
                {
                    Vec3b color = inputImage.Get<Vec3b>(j, i);
                    var ch0 = ((color.Item0 >> bitShift) << bitShift)  *scaleFactor;
                    var ch1 = ((color.Item1 >> bitShift) << bitShift) * scaleFactor;
                    var ch2 = ((color.Item2 >> bitShift) << bitShift) * scaleFactor;

                    color.Item0 = (byte)(ch0 > 255 ? 255 : ch0);
                    color.Item1 = (byte)(ch1 > 255 ? 255 : ch1);
                    color.Item2 = (byte)(ch2 > 255 ? 255 : ch2);

                    newImage.Set<Vec3b>(j, i, color);
                }
            }

            return newImage;

        }
    

        /// <summary>
        /// Converts stream into image data
        /// </summary>
        /// <param name="imageStream"></param>
        /// <returns></returns>
        private async Task StreamToMat(Stream imageStream)
        {
            byte[] imageData = new byte[imageStream.Length];
            await imageStream.ReadAsync(imageData, 0, (int)imageStream.Length);
            _imageMat = Mat.FromImageData(imageData, ImreadModes.Color);
        }

        /// <summary>
        /// Converts mat back into stream for azure uploading
        /// </summary>
        /// <param name="imageMat"></param>
        /// <returns></returns>
        private Stream MatToStream()
        {
            byte[] imageData = _imageMat.ToBytes();
            Stream imageStream = new MemoryStream(imageData);
            return imageStream;

        }
       
    }
}
