using Rocky.Domain.Interfaces.InquiryHeader;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;
using Rocky.Infra.Data.Scrutor;

namespace Rocky.Infra.Data.Repositories.InquiryHeader
{
    public class InquiryHeaderAsyncRepository : AsyncRepository<Domain.Entities.InquiryHeader, int>, IScoped, IInquiryHeaderAsyncRepository
    {
        public InquiryHeaderAsyncRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
