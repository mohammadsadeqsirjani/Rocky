using AutoMapper;
using Rocky.Domain.Entities;
using Rocky.Dto.ApplicationType;

namespace Rocky.Profiles
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
