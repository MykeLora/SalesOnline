using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Contract;
using Sales.Application.Service;
using Sales.Infraestructure.Interfaces;
using Sales.Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Ioc.CategoryDependecy
{
    public static class CategoryDependency 
    {
        public static  void AddCategoryDependency(this IServiceCollection services)
        {
            // Repositories

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // App services

            services.AddScoped<ICategoryService, CategoryNewService>();
        }
    }
}
