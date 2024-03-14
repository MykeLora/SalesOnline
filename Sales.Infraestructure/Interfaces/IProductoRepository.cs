using Sales.Domain.Entites;
using Sales.Domain.Repository;
using Sales.Infraestructure.Core;
using Sales.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Sales.Infraestructure.Interfaces
{

    public interface IProductoRepository : IBaseRepository<Producto>
    {
        List<ProductoModel> GetProductsByCategory(int categoryId);

    }
}
