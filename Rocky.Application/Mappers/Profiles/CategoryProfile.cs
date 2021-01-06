using AutoMapper;
using Rocky.Application.ViewModels.Dtos.Category;
using Rocky.Domain.Entities;

namespace Rocky.Application.Mappers.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryAddDto, Category>();
            CreateMap<CategoryEditDto, Category>();
            CreateMap<Category, CategoryEditDto>();
        }
    }
}
