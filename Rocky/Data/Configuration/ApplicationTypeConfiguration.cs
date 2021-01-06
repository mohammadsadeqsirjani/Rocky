using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocky.Data.Configuration.Common;
using Rocky.Domain.Entities;

namespace Rocky.Data.Configuration
{
    public class ApplicationTypeConfiguration : BaseEntityConfiguration<ApplicationType>
    {
        public override void Configure(EntityTypeBuilder<ApplicationType> builder)
        {
            builder?.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(256);

            base.Configure(builder);
        }
    }
}
