using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saritasa.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public virtual List<StorageFile>? StorageFiles { get; set; }
    }
}
