using Rocky.Application.ViewModels.Dtos.Common;

namespace Rocky.Application.ViewModels.Dtos.InquiryDetail
{
    public class InquiryDetailEditDto : EntityEditDto
    {
        public int InquiryHeaderId { get; set; }
        public int ProductId { get; set; }
    }
}
