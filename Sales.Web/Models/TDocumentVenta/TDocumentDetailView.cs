using Sales.Application.Models.Product;
using Sales.Application.Models.TDocumentVentas;

namespace Sales.Web.Models.TDocumentVenta
{
    public class TDocumentDetailView
    {

        public bool success { get; set; }
        public string message { get; set; }
        public TDocumentVentaGetModel data { get; set; }
    }
}
