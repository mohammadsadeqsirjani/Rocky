using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Persistence.Configuration.Common;

namespace Rocky.Infra.Data.Persistence.Configuration
{
    public class InquiryDetailConfiguration : BaseEntityConfiguration<InquiryDetail>
    {
        public override void Configure(EntityTypeBuilder<InquiryDetail> builder)
        {
            builder.ToTable("InquiryDetail");

            builder.HasOne(b => b.Product)
                .WithMany(b => b.InquiryDetails)
                .HasForeignKey(b => b.ProductId);

            builder.HasOne(b => b.InquiryHeader)
                .WithMany(b => b.InquiryDetails)
                .HasForeignKey(b => b.InquiryHeaderId);

            base.Configure(builder);
        }
    }
}
