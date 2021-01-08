using AutoMapper;
using Rocky.Application.ViewModels.Dtos.InquiryHeader;
using Rocky.Domain.Entities;

namespace Rocky.Application.Mappers.Profiles
{
    public class InquiryHeaderProfile : Profile
    {
        public InquiryHeaderProfile()
        {
            CreateMap<InquiryHeader, InquiryHeaderGetDto>();
        }
    }
}
