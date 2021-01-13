using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Persistence.Configuration.Common;

namespace Rocky.Infra.Data.Persistence.Configuration
{
    public class OrderHeaderConfiguration : BaseEntityConfiguration<OrderHeader>
    {
        public override void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.ToTable("OrderHeader");

            builder.Property(b => b.CreatedBy)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(b => b.OrderDate)
                .IsRequired();

            builder.Property(b => b.ShippingDate)
                .IsRequired();

            builder.Property(b => b.TotalOrderPrice)
                .IsRequired();

            builder.Property(b => b.OrderStatus)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.PaymentDate)
                .IsRequired();

            builder.Property(b => b.TransactionId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(b => b.StreetAddress)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.City)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(b => b.State)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(b => b.PostalCode)
                .IsRequired()
                .HasMaxLength(16);

            builder.Property(b => b.Fullname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(b => b.ApplicationUser)
                .WithMany(b => b.OrderHeaders)
                .HasForeignKey(b => b.CreatedBy);

            base.Configure(builder);
        }
    }
}
