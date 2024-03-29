using Sales.Application.Models.Category;
using Sales.Application.Models.Product;

namespace Sales.Web.Models.Product
{
    public class ProductListResult
    {

        public bool success { get; set; }
        public string? message { get; set; }
        public List<ProductGetModel>? data { get; set; }
    }
}
