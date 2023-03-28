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
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Caption).IsRequired().HasMaxLength(250);

            builder.HasOne(x => x.Product).WithMany(x => x.ProductImanges).HasForeignKey(x => x.ProdutId);
        }
    }
}
