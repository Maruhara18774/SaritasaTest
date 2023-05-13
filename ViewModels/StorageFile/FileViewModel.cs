using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.StorageFile
{
    public class FileViewModel
    {
        public Guid ID { get; set; }
        public string Path { get; set; }
        public bool DownloadOnce { get; set; }
    }
}
