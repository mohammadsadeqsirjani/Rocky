using Rocky.Application.ViewModels.Dtos.Common;

namespace Rocky.Application.ViewModels.Dtos.InquiryDetail
{
    public class InquiryDetailGetDto : EntityGetDto
    {
        public int InquiryHeaderId { get; set; }
        public int ProductId { get; set; }
    }
}
