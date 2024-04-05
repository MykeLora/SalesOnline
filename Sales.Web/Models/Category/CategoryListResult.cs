using Sales.Application.Models.Category;
using System.Drawing;

namespace Sales.Web.Models.Category
{
    public class CategoryListResult
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public List<CategoryResult> data { get; set; }
    }

}
