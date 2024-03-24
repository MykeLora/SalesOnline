using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.Product
{
    public abstract class ProductsDtoBase 
    {
        public int id { get; set; }
        public string? Marca { get; set; }
        public int CategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int? Stock {  get; set; }
    }
}
