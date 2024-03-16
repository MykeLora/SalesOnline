using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Models.TDocumentVentas
{
    public class TDocumentVentaGetModel
    {
        public int? TDocumentVentaId { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
