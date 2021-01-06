using AutoMapper;
using Rocky.Application.Dtos.Product;
using Rocky.Domain.Entities;

namespace Rocky.Application.Mappers.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductGetDto>()
                .ForMember(p => p.CategoryType, o => o.MapFrom(p => p.Category.Name))
                .ForMember(p => p.ApplicationType, o => o.MapFrom(p => p.ApplicationType.Name));

            CreateMap<Product, ProductUpsertDto>();
            CreateMap<ProductUpsertDto, Product>();
        }
    }
}
