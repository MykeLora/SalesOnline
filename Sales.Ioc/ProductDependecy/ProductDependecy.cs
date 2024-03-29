using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Contract;
using Sales.Application.Models.Product;
using Sales.Application.Service;
using Sales.Infraestructure.Interfaces;
using Sales.Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Ioc.ProductDependecy
{
    public static class ProductDependecy
    {
        public static void AddProductDependecy(this IServiceCollection services)
        {

            // Repositories

            services.AddScoped<IProductoRepository, ProductRepository>();

            // App Service

            services.AddScoped<IProductService, ProductService>();

        }
    }
}
