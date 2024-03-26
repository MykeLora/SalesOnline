using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.Product
{
    public class ProductsDtoAdd : ProductsDtoBase
    {
        public int ProductId { get; internal set; }
    }
}
