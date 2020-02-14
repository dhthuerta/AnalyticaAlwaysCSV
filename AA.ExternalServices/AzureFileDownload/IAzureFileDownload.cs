using AA.CrossDomain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.ExternalServices.AzureFileDownload
{
    public interface IAzureFileDownload
    {
        FileDTO GetFile();
    }
}
