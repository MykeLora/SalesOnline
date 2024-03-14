using Sales.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Entites
{
    public class Menu:BaseEntity
    {
        public int? IdMenu {  get; set; }
        public string nombre {  get; set; }
        public string icono {  get; set; }
        public string url { get; set;}
    }
}
