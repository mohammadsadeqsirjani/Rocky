using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Persistence.Configuration.Common;

namespace Rocky.Infra.Data.Persistence.Configuration
{
    public class ProductConfiguration : BaseEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.ShortDescription)
                .HasMaxLength(256);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.Picture)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            builder.HasOne(p => p.ApplicationType)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ApplicationTypeId);

            base.Configure(builder);
        }
    }
}
