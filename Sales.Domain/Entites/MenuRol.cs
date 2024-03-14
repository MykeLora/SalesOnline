using Sales.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Entites
{
    public class MenuRol : BaseEntity
    {
        public int? IdMenuRol { get; set; }
        public int IdMenu { get; set; }
        public int IdRol { get; set; }

    }
}
