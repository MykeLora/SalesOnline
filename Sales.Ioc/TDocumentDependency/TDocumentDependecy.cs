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
        public static void AddTDocumentDependecy( this IServiceCollection service)
        {
            //Repositories

            service.AddScoped<ITipoDocumentoVentaRepository, TipoDocumentoVentaRepository>();

            // App Services

            service.AddScoped<ITDocumentVentService, TDocumentServiceNew>();
        }
    }
}
