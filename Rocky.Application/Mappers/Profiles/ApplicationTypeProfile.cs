using AutoMapper;

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
