using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.Product
{
    public class ProductsDtoGetAll
    {
        public int CreationUser { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int CategoryID { get; set; }
        public int? UserMod { get; set; }
        public DateTime CreationDate { get; set; }

        public int ProductID { get; set; }
    }
}
