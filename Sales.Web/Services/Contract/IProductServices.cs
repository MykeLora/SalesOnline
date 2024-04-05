using Sales.Application.Dtos.Product;
using Sales.Application.Models.Product;
using Sales.Web.Models.Product;
using Sales.Web.Services.Core;

namespace Sales.Web.Services.Contract
{
    public interface IProductServices : IWebBaseService<ProductGetModel, ProductsDtoAdd, ProductsDtoUpdate>
    {
    }
}
