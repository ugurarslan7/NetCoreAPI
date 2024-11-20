﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.DBContext;
using Repositories.Products;

namespace Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {

            serviceDescriptors.AddDbContext<AppDbContext>(option =>
            {
                var connectionStrings = configuration.GetSection
                (ConnectionStringOption.Key).Get<ConnectionStringOption>();

                option.UseSqlServer(connectionStrings.SqlServer,
                    sqlServerOptionsAction =>
                    {
                        sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                    });
            });

            serviceDescriptors.AddScoped<IProductRepository, ProductRepository>();
            serviceDescriptors.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            serviceDescriptors.AddScoped<IUnitOfWork, UnitOfWork>();
            return serviceDescriptors;
        }
    }
}