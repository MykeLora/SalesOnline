using Sales.Application.Models.Category;

namespace Sales.Web.Models.Category
{
    public class CategoryListResult
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public List<CategoryGetModel>? data { get; set; }
    }
}
