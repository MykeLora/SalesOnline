using Sales.Application.Core;
using Sales.Application.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Contract
{
    public interface ICategoryService: IBaseServices<CategoryDtoAdd, CategoryDtoUpdate,CategoryRemoveDto >
    {

        object GetCategoryByCategoryID(int categoryID);
    }
}
