using Rocky.Domain.Interfaces.InquiryHeader;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;

namespace Rocky.Infra.Data.Repositories.InquiryHeader
{
    public class InquiryHeaderAsyncRepository : AsyncRepository<Domain.Entities.InquiryHeader, int>, IInquiryHeaderAsyncRepository
    {
        public InquiryHeaderAsyncRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
