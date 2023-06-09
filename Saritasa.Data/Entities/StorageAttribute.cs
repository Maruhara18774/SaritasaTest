﻿using Saritasa.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saritasa.Data.Entities
{
    public class StorageAttribute
    {
        public Guid ID { get; set; }
        public bool DownloadOnce { get; set; } = false;
        public Guid UserID { get; set; }
        public string? UpdateByUserID { get; set; }
        public FileStateEnum FileState { get; set; } = FileStateEnum.InUse;

        public virtual User? User { get; set; }
    }
}
