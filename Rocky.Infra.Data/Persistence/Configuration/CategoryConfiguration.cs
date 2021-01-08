using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Persistence.Configuration.Common;
using System.Collections.Generic;
using System.Linq;

namespace Rocky.Infra.Data.Persistence.Configuration
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(c => c.DisplayOrder)
                .IsRequired();

            base.Configure(builder);
        }

        public virtual void Seed(ApplicationDbContext context)
        {
            if (context.Category.Any()) return;

            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Stone",
                    DisplayOrder = 1
                },
                new Category
                {
                    Name = "Brick",
                    DisplayOrder = 2
                },
                new Category
                {
                    Name = "Natural Stone",
                    DisplayOrder = 3
                },
                new Category
                {
                    Name = "Concrete",
                    DisplayOrder = 4
                }
            };

            context.AddRange(categories);
            context.SaveChanges();
        }
    }
}
