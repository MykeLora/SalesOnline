using Sales.Application.Models.Category;
using Sales.Application.Models.TDocumentVentas;

namespace Sales.Web.Models.TDocumentVenta
{
    public class TDocumentListResult
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public List<TDocumentVentaGetModel>? data { get; set; }
    }
}
