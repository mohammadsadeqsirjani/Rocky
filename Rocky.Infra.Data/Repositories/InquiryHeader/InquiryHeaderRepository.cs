using Rocky.Domain.Interfaces.InquiryHeader;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;

namespace Rocky.Infra.Data.Repositories.InquiryHeader
{
    public class InquiryHeaderRepository : Repository<Domain.Entities.InquiryHeader, int>, IInquiryHeaderRepository
    {
        public InquiryHeaderRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
