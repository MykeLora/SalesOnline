using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.Product
{
    public class ProductsDtoRemove : ProductsDtoBase
    {
        public bool Eliminado {  get; set; }
        public int? ChangeUser { get; set; }
    }
}
