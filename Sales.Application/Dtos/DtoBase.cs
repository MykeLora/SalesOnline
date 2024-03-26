using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos
{
    public  class DtoBase
    {
        public int UserId { get; set; }
        public DateTime ChangeDate {  get; set; }
    }
}
