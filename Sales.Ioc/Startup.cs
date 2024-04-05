using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Contract;
using Sales.Application.Service;
using Sales.Infraestructure.Interfaces;
using Sales.Infraestructure.Repositories;
using Sales.Ioc.CategoryDependecy;
using Sales.Ioc.ProductDependecy;
using Sales.Ioc.TDocumentDependency;


public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
        // Aquí se agregan otros servicios
            services.AddProductDependecy();

            services.AddTDocumentDependecy();

            // Llama al método de extensión para agregar dependencias de categoría
            services.AddCategoryDependency();
        }

    }


