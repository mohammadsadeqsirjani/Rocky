using Rocky.Application.ViewModels.Dtos.InquiryDetail;
using Rocky.Application.ViewModels.Dtos.InquiryHeader;
using System.Collections.Generic;

namespace Rocky.Application.ViewModels
{
    public class InquiryVm
    {
        public InquiryHeaderGetDto InquiryHeader { get; set; }
        public IEnumerable<InquiryDetailGetDto> InquiryDetails { get; set; }
    }
}
