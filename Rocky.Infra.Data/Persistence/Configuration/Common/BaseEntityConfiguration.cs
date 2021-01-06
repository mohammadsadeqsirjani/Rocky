using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocky.Domain.Common;

namespace Rocky.Infra.Data.Persistence.Configuration.Common
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder?.HasKey(b => b.Id);
        }
    }
}
