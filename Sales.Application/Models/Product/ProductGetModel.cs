using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Models.Product
{
    public class ProductGetModel
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
