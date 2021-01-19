using AutoMapper;
using Rocky.Application.ViewModels.Dtos.OrderDetail;
using Rocky.Domain.Entities;

namespace Rocky.Application.Mappers.Profiles
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailGetDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
                .ForMember(d => d.PricePerSqft, o => o.MapFrom(s => s.Product.Price));
        }
    }
}
