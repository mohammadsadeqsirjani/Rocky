using AutoMapper;
using Rocky.Application.ViewModels.Dtos.InquiryHeader;
using Rocky.Domain.Entities;

namespace Rocky.Application.Mappers.Profiles
{
    public class InquiryProfile : Profile
    {
        public InquiryProfile()
        {
            CreateMap<InquiryHeader, InquiryHeaderGetDto>();
        }
    }
}
