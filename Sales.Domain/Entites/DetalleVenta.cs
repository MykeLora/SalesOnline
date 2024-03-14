using Sales.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Entites
{
    public class DetalleVenta:BaseEntity
    {
        public int? IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto {  get; set; }
        public int Cantidad { get; set; }
        public Decimal precio {  get; set; }
        public Decimal total {  get; set; }
    }
}
