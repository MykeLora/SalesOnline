using Sales.Application.Models.Category;
using Sales.Application.Models.Product;

namespace Sales.Web.Models.Product
{
    public class ProductListResponse
    {

        public bool success { get; set; }
        public string? message { get; set; }
        public List<ProductsViewResult> data { get; set; }
    }

    public class ProductsViewResult
    {
        public int productId { get; set; }
        public string? marca { get; set; }
        public int categoryId { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
        public string? description { get; set; }
    }
}
