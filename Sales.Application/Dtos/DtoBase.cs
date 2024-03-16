using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos
{
    public abstract class DtoBase
    {
        public int ChanceUser { get; set; }
        public DateTime ChangeDate {  get; set; }
    }
}
