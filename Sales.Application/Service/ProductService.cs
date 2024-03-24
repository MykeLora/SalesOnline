using Microsoft.Extensions.Logging;
using Sales.Application.Contract;
using Sales.Application.Core;
using Sales.Application.Dtos.Product;
using Sales.Domain.Entites;
using Sales.Infraestructure.Interfaces;

namespace Sales.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly IProductoRepository productRepository;

        public ProductService(ILogger<ProductService> logger,
                               IProductoRepository productRepository)
        {
            this.logger = logger;
            this.productRepository = productRepository;
        }

        public ServicesResult<ProductsDtoGetAll> Get(int Id)
        {
            ServicesResult<ProductsDtoGetAll> result = new ServicesResult<ProductsDtoGetAll>();

            try
            {
                var product = this.productRepository.GetEntity(Id);

                if (product != null)
                {
                    result.Data = new ProductsDtoGetAll()
                    {
                        id = product.id,
                        Description = product.Descripcion,
                        Marca = product.Marca,
                        CreateDate = product.FechaRegistro
                    };
                }
                else
                {
                    result.Success = false;
                    result.Message = "El producto no existe.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener el producto.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult<List<ProductsDtoGetAll>> GetAll()
        {
            ServicesResult<List<ProductsDtoGetAll>> result = new ServicesResult<List<ProductsDtoGetAll>>();

            try
            {
                var products = this.productRepository.GetEntities().Select(
                    products => new ProductsDtoGetAll()
                    {
                        id = products.id,
                        Price = products.Precio,
                        Stock = products.Stock,
                        Marca = products.Marca

                    }).ToList();

                result.Data = products;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener los productos.";
                this.logger.LogError(result.Message, ex.ToString());

            }

            return result;
        }

        public ServicesResult<ProductsDtoGetAll> Remove(ProductsDtoRemove RemoveDto)
        {
            ServicesResult<ProductsDtoGetAll> result = new ServicesResult<ProductsDtoGetAll>();

            try
            {
                this.productRepository.Remove(new Producto()
                {
                    id = RemoveDto.id,
                    IdUsuarioElimino = RemoveDto.IdUsuarioElimino,
                    FechaElimino = RemoveDto.FechaElimino
                });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al eliminar el producto.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult<ProductsDtoGetAll> Save(ProductsDtoAdd AddDto)
        {
            ServicesResult<ProductsDtoGetAll> result = new ServicesResult<ProductsDtoGetAll>();

            try
            {
                var validationResult = this.IsValid(AddDto);

                if (!validationResult.Success)
                {
                    result.Message = validationResult.Message;
                    return result;
                }

                this.productRepository.Save(new Producto()
                {
                    IdProducto = AddDto.id,
                    Descripcion = AddDto.Description,
                    IdUsuarioCreacion = AddDto.IdUsuarioCreacion,
                    Marca = AddDto.Marca,
                    Precio = AddDto.Price,
                    Stock = AddDto.Stock,
                    id = AddDto.CategoryId
                });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al guardar el producto.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult<ProductsDtoGetAll> Update(ProductsDtoUpdate UpdteDto)
        {
            ServicesResult<ProductsDtoGetAll> result = new ServicesResult<ProductsDtoGetAll>();

            try
            {
                var validationResult = this.IsValid(UpdteDto);

                if (!validationResult.Success)
                {
                    result.Message = validationResult.Message;
                    return result;
                }

                var product = this.productRepository.GetEntity(UpdteDto.Id);

                if (product == null)
                {
                    result.Success = false;
                    result.Message = "El producto no existe.";
                    return result;
                }

                product.Descripcion = UpdteDto.Description;
                product.Marca = UpdteDto.Marca;
                product.FechaMod = UpdteDto.FechaMod;
                product.IdUsuarioMod = UpdteDto.IdUsuarioMod;

                this.productRepository.Update(product);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al actualizar el producto.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        private ServicesResult<string> IsValid(ProductsDtoBase productsDtoBase)
        {
            ServicesResult<string> result = new ServicesResult<string>();

            if (string.IsNullOrEmpty(productsDtoBase.Marca))
            {
                result.Success = false;
                result.Message = "El nombre del producto es requerido.";
                return result;
            }

            if (productsDtoBase.Marca.Length > 15)
            {
                result.Success = false;
                result.Message = "El nombre del producto debe tener máximo 15 caracteres.";
                return result;
            }

            if (string.IsNullOrEmpty(productsDtoBase.Description))
            {
                result.Success = false;
                result.Message = "La descripción del producto es requerida.";
                return result;
            }

            if (productsDtoBase.Description.Length > 200)
            {
                result.Success = false;
                result.Message = "La descripción del producto debe tener máximo 200 caracteres.";
                return result;
            }

            if (this.productRepository.Exists(ca => ca.Marca == productsDtoBase.Marca))
            {
                result.Success = false;
                result.Message = $"El producto {productsDtoBase.Marca} ya existe.";
                return result;
            }

            return result;
        }
    }
}
