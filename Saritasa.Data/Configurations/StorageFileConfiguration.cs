using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saritasa.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saritasa.Data.Configuration
{
    public class StorageFileConfiguration : IEntityTypeConfiguration<StorageFile>
    {
        public void Configure(EntityTypeBuilder<StorageFile> builder)
        {
            builder.ToTable("Files");

            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.User).WithMany(x => x.StorageFiles).HasForeignKey(x => x.UserID);
        }
    }
}
