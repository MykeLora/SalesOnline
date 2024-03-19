using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.TDocumentVenta
{
    public class TDocumentDtoUpdate : TDocumentDtoBase
    {
        public DateTime? FechaMod { get; internal set; }
        public int? ChanceUser { get; internal set; }
    }
}
