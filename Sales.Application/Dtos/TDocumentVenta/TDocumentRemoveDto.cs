using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.TDocumentVenta
{
    public class TDocumentRemoveDto : TDocumentDtoBase
    {
        public bool Eliminado { get; set; } = false;
        public int? IdUsuarioElimino { get; internal set; }
        public DateTime? FechaElimino { get; internal set; }
    }
}
