using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.DBContext;
using Repositories.Products;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Products;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using Services.ExceptionHandlers;
using Services.Categories;

namespace Services.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {

            serviceDescriptors.AddScoped<IProductService, ProductService>();
            serviceDescriptors.AddScoped<ICategoryService, CategoryService>();

            serviceDescriptors.AddFluentValidationAutoValidation();

            serviceDescriptors.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            serviceDescriptors.AddAutoMapper(Assembly.GetExecutingAssembly());

            serviceDescriptors.AddExceptionHandler<CriticalExceptionHandler>();
            serviceDescriptors.AddExceptionHandler<GlobalExceptionHandler>();

            return serviceDescriptors;
        }
    }
}
