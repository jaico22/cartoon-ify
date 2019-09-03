using System;
using Core.FileProcessor;
using Infastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace cartoonify_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        private readonly IFileUploader _fileUploader;
        private FileProcessor _fileProcessor;

        public UploadsController(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
            _fileProcessor = new FileProcessor(_fileUploader);
        }

        // POST api/uploads
        [HttpPost]
        public async System.Threading.Tasks.Task PostAsync([FromForm(Name ="image")] IFormFile model)
        {
            var uri = await _fileProcessor.processAndUploadFile(new ImageFileStreamAndMetaData
            {
                FileName = model.FileName,
                FileStream = model.OpenReadStream()
            });


            Console.WriteLine("File retrieved");
        }
    }
}