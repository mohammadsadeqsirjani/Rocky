using AutoMapper;
using Rocky.Domain.Entities;
using Rocky.Dto.Product;

namespace Rocky.Profiles
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
