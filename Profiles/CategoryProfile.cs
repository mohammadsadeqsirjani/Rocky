using AutoMapper;
using Rocky.Dto.Category;
using Rocky.Models;

namespace Rocky.Profiles
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
