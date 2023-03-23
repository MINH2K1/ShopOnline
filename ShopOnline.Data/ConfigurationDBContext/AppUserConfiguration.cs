using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Data.ConfigurationDBContext
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            {
                builder.ToTable("AppUsers");
                builder.Property(x => x.FirstName).HasMaxLength(250).IsRequired();
                builder.Property(x => x.LastName).HasMaxLength(250).IsRequired();
                builder.Property(x => x.Dob).IsRequired();
            }
        }
    }
}
