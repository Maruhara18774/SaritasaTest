﻿using Saritasa.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saritasa.Data.Entities
{
    public class StorageFile: StorageAttribute
    {
        public string? Url { get; set; }
    }
}
