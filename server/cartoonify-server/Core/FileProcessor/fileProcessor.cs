using Core.ImageProcessing;
using Infastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Core.FileProcessor
{
    public class FileProcessor
    {
        private readonly IFileUploader _fileUploader;
        private readonly ImageProcessor _imageProcessor; 
        public FileProcessor(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
            _imageProcessor = new ImageProcessor();
        }

        public async Task<string> processAndUploadFile(ImageFileStreamAndMetaData imageFileStreamAndMetaData)
        {
            // TODO: process file Stream
            var processedImageStream = await _imageProcessor.CartoonifyImage(imageFileStreamAndMetaData.FileStream);
            
            // Upload file 
            return await _fileUploader.UploadAsync(imageFileStreamAndMetaData.FileName, processedImageStream);
        }
    }
}
