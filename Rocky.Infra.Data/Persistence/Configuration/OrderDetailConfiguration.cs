using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Persistence.Configuration.Common;

namespace Rocky.Infra.Data.Persistence.Configuration
{
    public class OrderDetailConfiguration : BaseEntityConfiguration<OrderDetail>
    {
        public override void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetail");

            builder.Property(b => b.OrderHeaderId)
                .IsRequired();

            builder.Property(b => b.ProductId)
                .IsRequired();

            builder.HasOne(b => b.OrderHeader)
                .WithMany(b => b.OrderDetails)
                .HasForeignKey(b => b.OrderHeaderId);

            builder.HasOne(b => b.Product)
                .WithMany(b => b.OrderDetails)
                .HasForeignKey(b => b.ProductId);

            base.Configure(builder);
        }
    }
}
