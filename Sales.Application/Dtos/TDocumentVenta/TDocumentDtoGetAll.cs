using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.TDocumentVenta
{
    public class TDocumentDtoGetAll : DtoBase
    {
        public string? Description { get; internal set; }
    }
}
