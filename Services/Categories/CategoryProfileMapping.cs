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

namespace Services.Categories
{
    public class CategoryProfileMapping:Profile
    {
        public CategoryProfileMapping()
        {
            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<CreateProductRequest, Category>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(p => p.Name.ToLowerInvariant()));

            CreateMap<UpdateProductRequest, Category>().ForMember(dest => dest.Name,
              opt => opt.MapFrom(p => p.Name.ToLowerInvariant()));
        }
    }
}
