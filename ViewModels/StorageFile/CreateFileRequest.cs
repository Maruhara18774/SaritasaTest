using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.StorageFile
{
    public class CreateFileRequest
    {
        public IFormFile File { get; set; }
        public bool DownloadOnce { get; set; }
    }
}
