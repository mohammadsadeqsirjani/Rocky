using AutoMapper;
using Rocky.Dto.ApplicationType;
using Rocky.Models;

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
