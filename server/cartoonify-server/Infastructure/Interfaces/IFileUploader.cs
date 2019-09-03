using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Interfaces
{
    public interface IFileUploader
    {
        /// <summary>
        /// Uploads File to remove Server. Returns URI of file
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns>Uri of file</returns>
        Task<string> UploadAsync(string fileName, Stream fileStream);

    }
}
