using Microsoft.Extensions.Logging;
using Sales.Domain.Entites;
using Sales.Infraestructure.Context;
using Sales.Infraestructure.Core;
using Sales.Infraestructure.Interfaces;
using Sales.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infraestructure.Repositories
{
    public class ProductRepository : BaseRepository<Producto>, IProductoRepository
    {
        private readonly SalesContext context;
        private readonly ILogger<ProductRepository> logger;

        public ProductRepository(SalesContext context, ILogger<ProductRepository> logger) : base(context)
        {
            this.context = context;
            this.logger = logger;
        }

        public List<ProductoModel> GetProductsByCategory(int categoryId)
        {
            List<ProductoModel> products = new List<ProductoModel>();

            try
            {
                // Corrected and optimized query:
                products = (from pro in this.context.Producto
                            join ca in this.context.Categoria on pro.IdCategoria equals ca.id
                            where pro.IdCategoria == categoryId
                            select new ProductoModel()
                            {
                                Marca = pro.Marca,
                                Nombre = ca.nombre,
                                Stock = (int)pro.Stock,
                                Precio = (decimal)pro.Precio,
                                IdProducto = pro.id,
                            }).ToList();

                return products;
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error obteniendo los productos", ex.ToString());
                // Aquí puedes decidir qué hacer en caso de error, como lanzar una excepción o devolver una lista vacía
                throw; // O lanzar la excepción para propagarla
            }

            // Agrega una declaración de retorno fuera del bloque try-catch
            // Esta declaración se ejecutará si no hay excepciones
            return products;
        }

        public override void Save(Producto entity)
        {

            try
            {
                // Check for existing product id (optional)
                if (context.Producto.Any(p => p.id == entity.id))
                {
                    this.logger.LogWarning("Ya existe un producto con ese id");
                }

                this.context.Producto.Add(entity);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error al crear el producto", ex.ToString());
            }
        }

        public override Producto GetEntity(int id)
        {
            return this.context.Producto.Find(id);
        }

        public override void Update(Producto entity)
        {
            try
            {
                var productToUpdate = this.GetEntity(entity.id);

                if (productToUpdate == null)
                {
                    this.logger.LogWarning("El producto no existe");
                }

                productToUpdate.Descripcion = entity.Descripcion;
                productToUpdate.FechaMod = entity.FechaMod;
                productToUpdate.IdCategoria = entity.IdCategoria;
                productToUpdate.Precio = entity.Precio;
                productToUpdate.Stock = entity.Stock;
                productToUpdate.Marca = entity.Marca;
                productToUpdate.IdUsuarioMod = entity.IdUsuarioMod;

                this.context.Producto.Update(productToUpdate);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error al actualizar el producto", ex.ToString());
            }
        }

        public override List<Producto> GetEntities()
        {
            return base.GetEntities().Where(p => !p.Eliminado).ToList();
        }

        public override List<Producto> FinAll(Func<Producto, bool> filter)
        {
            return this.context.Producto.Where(filter).ToList();
        }

        public override bool Exists(Func<Producto, bool> filter)
        {
            return this.context.Producto.Any(filter);
        }

     
    }
}
