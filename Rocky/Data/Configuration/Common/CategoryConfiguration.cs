using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocky.Models;

namespace Rocky.Data.Configuration.Common
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(c => c.DisplayOrder)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
