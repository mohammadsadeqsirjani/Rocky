using Rocky.Domain.Interfaces.InquiryDetail;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;

namespace Rocky.Infra.Data.Repositories.InquiryDetail
{
    public class InquiryDetailAsyncRepository : AsyncRepository<Domain.Entities.InquiryDetail, int>, IInquiryDetailAsyncRepository
    {
        public InquiryDetailAsyncRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
