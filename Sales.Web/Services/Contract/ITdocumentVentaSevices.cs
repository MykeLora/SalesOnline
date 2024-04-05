using Sales.Application.Dtos.TDocumentVenta;
using Sales.Application.Models.TDocumentVentas;
using Sales.Web.Services.Core;

namespace Sales.Web.Services.Contract
{
    public interface ITdocumentVentaSevices : IWebBaseService<TDocumentVentaGetModel, TDocumentDtoAdd, TDocumentDtoUpdate>
    {
    }
}
