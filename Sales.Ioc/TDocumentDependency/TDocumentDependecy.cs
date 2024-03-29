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

namespace Sales.Ioc.TDocumentDependency
{
    public static class TDocumentDependecy
    {
        public static void AddTDocumentDependecy( this IServiceCollection services)
        {
            //Repositories

            services.AddScoped<ITipoDocumentoVentaRepository, TipoDocumentoVentaRepository>();

            // App Services

            services.AddScoped<ITDocumentVentService, TDocumentServiceNew>();
        }
    }
}
