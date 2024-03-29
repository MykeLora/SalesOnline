using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.Product
{
    public  class ProductsDtoBase : DtoBase
    {
        public string? Marca { get; set; }
        public int CategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? Descripcion { get; set; }
        public int? Stock { get; set; }
    }
}
