using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saritasa.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saritasa.Data.Configurations
{
    public class StorageTextConfiguration : IEntityTypeConfiguration<StorageText>
    {
        public void Configure(EntityTypeBuilder<StorageText> builder)
        {
            builder.ToTable("Texts");

            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.User).WithMany(x => x.StorageTexts).HasForeignKey(x => x.UserID);
        }
    }
}
