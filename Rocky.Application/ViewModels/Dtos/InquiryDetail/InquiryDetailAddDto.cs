using Rocky.Application.ViewModels.Dtos.Common;

namespace Rocky.Application.ViewModels.Dtos.InquiryDetail
{
    public class InquiryDetailAddDto : EntityAddDto
    {
        public int InquiryHeaderId { get; set; }
        public int ProductId { get; set; }
    }
}
