using Rocky.Domain.Interfaces.InquiryDetail;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;

namespace Rocky.Infra.Data.Repositories.InquiryDetail
{
    public class InquiryDetailRepository : Repository<Domain.Entities.InquiryDetail>, IInquiryDetailRepository
    {
        public InquiryDetailRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
