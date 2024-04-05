using Sales.Application.Dtos.Category;
using Sales.Application.Models.Category;
using Sales.Web.Models.Category;
using Sales.Web.Services.Core;

namespace Sales.Web.Services.Contract
{
    public interface ICategoryServices : IWebBaseService<CategoryGetModel, CategoryDtoAdd, CategoryDtoUpdate>
    {
    }
}
