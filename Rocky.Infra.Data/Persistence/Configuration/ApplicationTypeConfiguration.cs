using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Persistence.Configuration.Common;

namespace Rocky.Infra.Data.Persistence.Configuration
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
