﻿using AutoMapper;
using Rocky.Domain.Entities;
using Rocky.Dto.Category;

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