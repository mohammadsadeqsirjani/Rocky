using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Persistence.Configuration.Common;
using System.Collections.Generic;
using System.Linq;

namespace Rocky.Infra.Data.Persistence.Configuration
{
    public class ApplicationTypeConfiguration : BaseEntityConfiguration<ApplicationType>
    {
        public override void Configure(EntityTypeBuilder<ApplicationType> builder)
        {
            builder.ToTable("ApplicationType");

            builder?.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(256);

            base.Configure(builder);
        }

        public virtual void Seed(ApplicationDbContext context)
        {
            if (context.ApplicationType.Any()) return;

            var applicationTypes = new List<ApplicationType>
            {
                new ApplicationType
                {
                    Name = "Sidewalk"
                },
                new ApplicationType
                {
                    Name = "Driveway"
                },
                new ApplicationType
                {
                    Name = "Patio"
                },
                new ApplicationType
                {
                    Name = "Edging"
                }
            };

            context.AddRange(applicationTypes);
            context.SaveChanges();
        }
    }
}
