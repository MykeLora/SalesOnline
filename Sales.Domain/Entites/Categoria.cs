using Sales.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Entites
{
    public class Categoria : BaseEntity
    {

        public string? Descripcion { get; set; }
        public string? nombre {  get; set; }
        public bool? EsActivo {  get; set; }
    }
}
