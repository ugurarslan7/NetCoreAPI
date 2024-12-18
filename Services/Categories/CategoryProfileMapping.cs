using AutoMapper;
using Repositories.Products;
using Services.Products.Create;
using Services.Products.Update;
using Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Categories;
using Services.Categories.Dto;
using Services.Categories.Create;

namespace Services.Categories
{
    public class CategoryProfileMapping:Profile
    {
        public CategoryProfileMapping()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();

            CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

            CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        }
    }
}
