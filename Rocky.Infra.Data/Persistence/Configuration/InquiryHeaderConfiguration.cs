using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Persistence.Configuration.Common;

namespace Rocky.Infra.Data.Persistence.Configuration
{
    public class InquiryHeaderConfiguration : BaseEntityConfiguration<InquiryHeader>
    {
        public override void Configure(EntityTypeBuilder<InquiryHeader> builder)
        {
            builder.ToTable("InquiryHeader");

            builder.Property(b => b.Fullname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasOne(b => b.ApplicationUser)
                .WithMany(b => b.InquiryHeaders)
                .HasForeignKey(b => b.ApplicationUserId);

            base.Configure(builder);
        }
    }
}
