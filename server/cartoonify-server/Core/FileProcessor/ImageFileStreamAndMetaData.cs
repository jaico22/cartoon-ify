using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.FileProcessor
{
    public class ImageFileStreamAndMetaData
    {
        public string FileName { get; set; }

        public Stream FileStream { get; set; }
    }
}
