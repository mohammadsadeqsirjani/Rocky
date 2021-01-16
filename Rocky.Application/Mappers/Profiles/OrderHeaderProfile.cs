using AutoMapper;
using Rocky.Application.ViewModels.Dtos.OrderHeader;
using Rocky.Domain.Entities;

namespace Rocky.Application.Mappers.Profiles
{
    public class OrderHeaderProfile : Profile
    {
        public OrderHeaderProfile()
        {
            CreateMap<OrderHeader, OrderHeaderGetDto>();
        }
    }
}
