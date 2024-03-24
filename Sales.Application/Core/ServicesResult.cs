using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Core
{
    public class ServicesResult<TData>
    {
        public ServicesResult() 
        {
            this.Success = true;
        }

        public bool Success { get; set; }

        public string? Message { get; set; }

        public dynamic? Data { get; set; }
    }
}
