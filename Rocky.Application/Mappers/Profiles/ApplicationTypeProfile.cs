using AutoMapper;
using Rocky.Application.Dtos.ApplicationType;
using Rocky.Domain.Entities;

namespace Rocky.Application.Mappers.Profiles
{
    public class ApplicationTypeProfile : Profile
    {
        public ApplicationTypeProfile()
        {
            CreateMap<ApplicationType, ApplicationTypeGetDto>();
            CreateMap<ApplicationTypeAddDto, ApplicationType>();
            CreateMap<ApplicationTypeEditDto, ApplicationType>();
            CreateMap<ApplicationType, ApplicationTypeEditDto>();
        }
    }
}
