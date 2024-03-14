using Sales.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infraestructure.Models
{

    public class ProductoModel
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int CategoriaId { get; set; }
        public decimal Precio { get; set; }
        public string Marca { get; set; }
        public int Stock { get; set; }

        public Categoria Categoria { get; set; } // Navigation pr

    }
}
