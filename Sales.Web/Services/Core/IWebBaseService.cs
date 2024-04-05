using Microsoft.Exchange.WebServices.Data;
using Sales.Application.Core;

namespace Sales.Web.Services.Core
{

    public interface IWebBaseService<TResult, TAddModel, TUpdateModel>
    {
        Task<ServicesResult<IEnumerable<TResult>>> Get();
        Task<ServicesResult<TResult>> GetById(int id);
        Task<ServicesResult<bool>> Save(TAddModel model);
        Task<ServicesResult<bool>> Update(TUpdateModel model);
    }
}
