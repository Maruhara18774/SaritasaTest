using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Saritasa.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saritasa.Data.Configuration;

namespace Saritasa.Data.EF
{
    public class LocalDBContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<StorageFile>? StorageFiles { get; set; }
        public LocalDBContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StorageFileConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
