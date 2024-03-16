using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Validations
{
     public static class CategoryValidation
    {
        public static void CommonValidation(DateTime FechaRegistro, DateTime FechaMod, string nombre, string Descripcion, int CategoryId, IConfiguration configuration)
        {
            if(string.IsNullOrEmpty(Descripcion))
            {
                string ErrorMessage = $"{configuration["MensajeValidaciones: La DescripcionEsRequerida"]}";
                throw new Exception(ErrorMessage);
            }
        }
    }
}
