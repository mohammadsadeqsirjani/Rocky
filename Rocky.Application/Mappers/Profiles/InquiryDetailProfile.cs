using AutoMapper;
using Rocky.Application.ViewModels.Dtos.InquiryDetail;
using Rocky.Domain.Entities;

namespace Rocky.Application.Mappers.Profiles
{
    public class InquiryDetailProfile : Profile
    {
        public InquiryDetailProfile()
        {
            CreateMap<InquiryDetail, InquiryDetailGetDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
                .ForMember(d => d.ProductPrice, o => o.MapFrom(s => s.Product.Price));
        }
    }
}
