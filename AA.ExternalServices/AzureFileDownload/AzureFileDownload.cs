using AA.CrossDomain;
using AA.CrossDomain.DTOs;
using AA.ExternalServices.AzureFileDownload;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;

namespace AA.ExternalServices.AzureFileDownload
{
    public class AzureFileDownload: IAzureFileDownload
    {
        private readonly IConfiguration config;

        public AzureFileDownload(IConfiguration _config)
        {
            config = _config;
        }

        public FileDTO GetFile()
        {
            var urlFile = config[Constants.FILE_URL_KEY];

            WebClient client = new WebClient();
            var fileString = client.DownloadString(new Uri(urlFile));

            var fileDto = new FileDTO()
            {
                FileString = fileString,
                Length = fileString.Length
            };

            return fileDto;
        }
    }
}
