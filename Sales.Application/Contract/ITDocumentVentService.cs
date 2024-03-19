using Sales.Application.Core;
using Sales.Application.Dtos.Category;
using Sales.Application.Dtos.TDocumentVenta;
using Sales.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Contract
{
    public interface ITDocumentVentService : IBaseServices<TDocumentDtoAdd, TDocumentDtoUpdate, TDocumentRemoveDto >
    {
        object GetTDocumentByTDocumentID(int TDocumentID);
    }
}
