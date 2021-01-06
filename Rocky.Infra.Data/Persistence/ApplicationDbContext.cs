using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Persistence.Configuration;

namespace Rocky.Infra.Data.Persistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationTypeConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
