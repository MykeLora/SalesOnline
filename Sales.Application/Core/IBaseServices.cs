using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Core
{
    public interface IBaseServices<TDtoAddModel, TDtoUpdateModel, TDtoRemove>
    {
        ServicesResult GetAll();
        ServicesResult GetById(int id);
        ServicesResult Save(TDtoAddModel dtoAdd);
        ServicesResult Update(TDtoUpdateModel dtoUpdate);
        ServicesResult Remove(TDtoRemove dtoRemove);
    }
}
