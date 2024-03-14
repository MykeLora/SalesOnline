using Sales.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Entites
{
    public class TipoDocumentoVenta : BaseEntity
    {
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }

    }
}
