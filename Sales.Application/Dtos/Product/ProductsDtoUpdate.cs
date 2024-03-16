using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.Product
{
    public class ProductsDtoUpdate : ProductsDtoBase
    {
        public DateTime? FechaMod {  get; set; }    
        public int IdUsuarioMod {  get; set; }
        public int Id { get;}
    }
}
