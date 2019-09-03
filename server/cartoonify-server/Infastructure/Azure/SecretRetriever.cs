using System;
using System.Collections.Generic;
using System.Text;

namespace Infastructure.Azure
{
    public class SecretRetriever
    {
        public string GetBlobStorageConnectionString()
        {
            string storageConnectionString = Environment.GetEnvironmentVariable("AZURE_STRG_CONN_STR");
            return storageConnectionString;
        }
    }
}
