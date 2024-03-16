using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.Category
{
    public class CategoryRemoveDto : CategoryDtoBase
    {
        public CategoryRemoveDto() 
        {
            this.Eliminado = false;
            this.FechaElimino = DateTime.Now;

        }  
        public bool Eliminado { get; set; } 
        public int CategoryId { get; set; }
        public int IdUsuarioElimino {  get; set; }
        public DateTime FechaElimino { get; set; }

    }
}
