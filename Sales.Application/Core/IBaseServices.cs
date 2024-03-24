using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Core
{
    public interface IBaseServices<TDtoAdd, TDtoUpdate, TDtoRemove, TModel>
    {
        ServicesResult<List<TModel>> GetAll();
        ServicesResult<TModel> Get(int Id);
        ServicesResult<TModel> Save(TDtoAdd AddDto);
        ServicesResult<TModel> Update(TDtoUpdate UpdteDto);
        ServicesResult<TModel> Remove(TDtoRemove RemoveDto);
    }
}
