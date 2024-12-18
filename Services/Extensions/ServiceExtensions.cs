using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Categories;
using Services.ExceptionHandlers;
using Services.Filters;
using Services.Products;
using System.Reflection;

namespace Services.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {

            //standart olan entity filtresini kapattık
            serviceDescriptors.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            serviceDescriptors.AddScoped<IProductService, ProductService>();
            serviceDescriptors.AddScoped<ICategoryService, CategoryService>();
           
            serviceDescriptors.AddScoped(typeof(NotFoundFilter<,>));

            serviceDescriptors.AddFluentValidationAutoValidation();

            serviceDescriptors.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            serviceDescriptors.AddAutoMapper(Assembly.GetExecutingAssembly());

            serviceDescriptors.AddExceptionHandler<CriticalExceptionHandler>();
            serviceDescriptors.AddExceptionHandler<GlobalExceptionHandler>();

            return serviceDescriptors;
        }
    }
}
