using Rocky.Domain.Interfaces.InquiryDetail;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;
using Rocky.Infra.Data.Scrutor;

namespace Rocky.Infra.Data.Repositories.InquiryDetail
{
    public class InquiryDetailRepository : Repository<Domain.Entities.InquiryDetail, int>, IScoped, IInquiryDetailRepository
    {
        public InquiryDetailRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
