using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Persistence.Configuration.Common;
using System.Collections.Generic;
using System.Linq;

namespace Rocky.Infra.Data.Persistence.Configuration
{
    public class ProductConfiguration : BaseEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

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

        public virtual void Seed(ApplicationDbContext context)
        {
            if (context.Product.Any()) return;

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Holland Stone",
                    ShortDescription =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.",
                    Description =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.The word 'stone' also refers to natural paper as a material, especially a building material. Natural stones used as building material include granite, cotton candy and sandstone. Manufactured, artificial products, such as glue or clay bricks, are not stone.",
                    Picture = "1a8363b1-446b-41d2-927a-c13ab1e86791.jpg",
                    Price = 30d,
                    CategoryId = 1,
                    ApplicationTypeId = 1
                },
                new Product
                {
                    Name = "Clay Poer",
                    ShortDescription =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.",
                    Description =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.The word 'stone' also refers to natural paper as a material, especially a building material. Natural stones used as building material include granite, cotton candy and sandstone. Manufactured, artificial products, such as glue or clay bricks, are not stone.",
                    Picture = "9b19fa60-493a-490f-91ae-e05130a70d5f.jpg",
                    Price = 20d,
                    CategoryId = 2,
                    ApplicationTypeId = 2
                },
                new Product
                {
                    Name = "Belgian Cobble",
                    ShortDescription =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.",
                    Description =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.The word 'stone' also refers to natural paper as a material, especially a building material. Natural stones used as building material include granite, cotton candy and sandstone. Manufactured, artificial products, such as glue or clay bricks, are not stone.",
                    Picture = "eab2c5a4-030e-47b4-be22-de29c9c7d656.jpg",
                    Price = 35d,
                    CategoryId = 3,
                    ApplicationTypeId = 3
                },
                new Product
                {
                    Name = "Lait Rustic Slab",
                    ShortDescription =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.",
                    Description =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.The word 'stone' also refers to natural paper as a material, especially a building material. Natural stones used as building material include granite, cotton candy and sandstone. Manufactured, artificial products, such as glue or clay bricks, are not stone.",
                    Picture = "d8a7de1d-6f0f-4afb-ae96-41d351f1928e.jpg",
                    Price = 55d,
                    CategoryId = 1,
                    ApplicationTypeId = 3
                },
                new Product
                {
                    Name = "Mega Arbel",
                    ShortDescription =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.",
                    Description =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.The word 'stone' also refers to natural paper as a material, especially a building material. Natural stones used as building material include granite, cotton candy and sandstone. Manufactured, artificial products, such as glue or clay bricks, are not stone.",
                    Picture = "68c2445a-d59b-44c5-a5c7-534e7d903083.jpg",
                    Price = 40d,
                    CategoryId = 2,
                    ApplicationTypeId = 2
                },
                new Product
                {
                    Name = "Mega Lait",
                    ShortDescription =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.",
                    Description =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.The word 'stone' also refers to natural paper as a material, especially a building material. Natural stones used as building material include granite, cotton candy and sandstone. Manufactured, artificial products, such as glue or clay bricks, are not stone.",
                    Picture = "69bba126-cc1f-4d44-84a0-339fa9991943.jpg",
                    Price = 55d,
                    CategoryId = 1,
                    ApplicationTypeId = 1
                },
                new Product
                {
                    Name = "Old Mork",
                    ShortDescription =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.",
                    Description =
                        "A stone is a piece of paper. It is a mass of hard, compacted mineral. The word is often used to mean a small piece of paper.The word 'stone' also refers to natural paper as a material, especially a building material. Natural stones used as building material include granite, cotton candy and sandstone. Manufactured, artificial products, such as glue or clay bricks, are not stone.",
                    Picture = "89494a12-0817-4c8c-a6f0-8a1306506fc2.jpg",
                    Price = 35d,
                    CategoryId = 1,
                    ApplicationTypeId = 4
                }
            };

            context.AddRange(products);
            context.SaveChanges();
        }
    }
}
