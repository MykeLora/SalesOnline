using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Core
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            this.FechaRegistro = DateTime.Now;
            this.Eliminado = false;
        }
        public int id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaMod { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public int? IdUsuarioMod { get; set; }
        public int? IdUsuarioElimino { get; set; }
        public DateTime? FechaElimino { get; set; }
        public bool Eliminado { get; set; }


    }
}
