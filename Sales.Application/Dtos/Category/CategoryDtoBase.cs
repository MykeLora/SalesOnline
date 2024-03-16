using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.Category
{
    public  class CategoryDtoBase : DtoBase
    {

        public string? Name { get; set; }
        public string? Description { get; set; }
        public int IdUsuarioCreacion {  get; set; }
        public DateTime? FechaMod {  get; set; }
    }
}

