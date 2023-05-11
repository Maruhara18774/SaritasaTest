using Saritasa.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saritasa.Data.Entities
{
    public class StorageFile
    {
        public Guid ID { get; set; }
        public bool DownloadOnce { get; set; } = false;
        public FileType Type { get; set; }
        public string? Url { get; set; }
        public Guid UserID { get; set; }
        public string? UpdateByUserID { get; set; }

        public virtual User? User { get; set; }
    }
}
