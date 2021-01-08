using Rocky.Application.ViewModels.Dtos.Common;
using System;

namespace Rocky.Application.ViewModels.Dtos.InquiryHeader
{
    public class InquiryHeaderGetDto : EntityGetDto
    {
        public string ApplicationUserId { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime InquiryDate { get; set; }
    }
}
