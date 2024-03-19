using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Exceptions
{
    public class ProductException : Exception
    {
        public ProductException(string Message): base(Message)
        {
        }
    }

}
