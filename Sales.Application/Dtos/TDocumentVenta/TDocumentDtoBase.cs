using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.TDocumentVenta
{
    public class TDocumentDtoBase : DtoBase
    {
        public string? Descripcion { get; set; }
        public bool? esActivo {  get; set; }
    }
}
