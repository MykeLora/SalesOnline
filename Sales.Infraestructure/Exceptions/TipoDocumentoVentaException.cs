using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infraestructure.Exceptions
{
    public class TipoDocumentoVentaException : Exception
    {
        public TipoDocumentoVentaException()
            : base("Ocurrió un error relacionado con los tipos de documentos de venta.")
        {
        }

        public TipoDocumentoVentaException(string message)
            : base(message)
        {
        }

        public TipoDocumentoVentaException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public int? TipoDocumentoVentaId { get; set; }

        public string CodigoTipoDocumentoVenta { get; set; }

        public string DescripcionTipoDocumentoVenta { get; set; }
    }
}
