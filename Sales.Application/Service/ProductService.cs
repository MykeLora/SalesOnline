using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sales.Application.Contract;
using Sales.Application.Core;
using Sales.Application.Dtos.Category;
using Sales.Application.Dtos.Product;
using Sales.Application.Models.Category;
using Sales.Application.Models.Product;
using Sales.Application.Reponse;
using Sales.Domain.Entites;
using Sales.Infraestructure.Interfaces;
using Sales.Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly ProductRepository productRepository;
        private readonly IConfiguration configuration;


        public ProductService(IProductoRepository productRepository, ILogger<ProductService> logger, IConfiguration configuration)
        {

            this.productRepository = (ProductRepository?)productRepository;
            this.logger = logger;
            this.configuration = configuration;

        }


        // validaciones bases para ser reutilizadas por los metodos //
        private ServicesResult ValidateProductCommon(ProductsDtoBase dto)
        {
            ServicesResult result = new ServicesResult();


            if (string.IsNullOrEmpty(dto.Marca))
            {
                result.Success = false;
                result.Message = "El producto es requerido.";
                return result;
            }
            if (dto.Marca.Length > 15)
            {
                result.Success = false;
                result.Message = "El nombre del producto debe tener 15 carácteres.";
                return result;
            }
            if (string.IsNullOrEmpty(dto.Description))
            {
                result.Success = false;
                result.Message = "El producto es requerido.";
                return result;

            }
            if (dto.Description.Length > 200)
            {
                result.Success = false;
                result.Message = "La descripción del producto debe tener 200 carácteres.";
                return result;
            }

            if (this.productRepository.Exists(ca => ca.Marca == dto.Marca))
            {
                result.Success = false;
                result.Message = $"El producto {dto.Marca} ya existe.";
                return result;
            }

            this.productRepository.Save(new Domain.Entites.Producto()
            {
                Marca = dto.Marca,
                FechaRegistro = dto.CreateDate,
                id = dto.ProductId,
                Descripcion = dto.Description,
                Precio = dto.Price,
                Stock  = dto.Stock,

              
            });


            return result;
        }


        public ServicesResult GetAll()
        {
            ServicesResult result = new ServicesResult();

            try
            {
                var product = this.productRepository.GetEntities().Select(
                    products => new ProductsDtoGetAll()
                    {
                        ProductID = products.id,
                        CreationDate = products.FechaRegistro,
                        CreationUser = products.IdUsuarioCreacion,
                        CategoryID = products.id,
                        Marca = products.Marca,
                        Stock = products.Stock

                    }).ToList();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener los productos";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult GetById(int id)
        {
            ServicesResult result = new ServicesResult();

            try
            {
                var products = this.productRepository.GetEntity(id);

                result.Data = new ProductGetModel()
                {
                    ProductId = products.id,
                    Description = products.Descripcion,
                    Name = products.Marca,
                    CreateDate = products.FechaRegistro
                };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener el producto";
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public ServicesResult Remove(ProductsDtoRemove dtoRemove)
        {
            ServicesResult result = new ServicesResult();
            ServicesResult validation = ValidateProductCommon(dtoRemove);

            if (!validation.Success)
            {
                result.Message = validation.Message;
                result.Success = false;
                return result;

            }

            try
            {
                Producto producto = new Producto()
   
                {
                    id = dtoRemove.ProductId,
                    Eliminado = dtoRemove.Eliminado,
                    IdUsuarioElimino = dtoRemove.IdUsuarioElimino,
                    FechaElimino = dtoRemove.FechaElimino,

                };

                this.productRepository.Remove(producto);

                result.Message = "El producto ha sido removido";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al eliminar el producto";
                this.logger.LogError(result.Message, ex.ToString());

            }
            return result;
        }

        public ServicesResult Save(ProductsDtoAdd dtoAdd)
        {
            ProductResponse result = new ProductResponse();

            ServicesResult validation = ValidateProductCommon(dtoAdd);

            if (!validation.Success)
            {
                result.Message = validation.Message;
                result.Success = false;
                return result;
            }
            try
            {
                Producto product = new Producto()
                {
                    Descripcion = dtoAdd.Description,
                    IdUsuarioCreacion = dtoAdd.IdUsuarioCreacion,
                    Marca = dtoAdd.Marca,
                    Precio = dtoAdd.Price

                };

                this.productRepository.Save(product);

                result.Message = this.configuration["MensajesProductSuccess:AddSuccessMessage"];
                result.ProductId = product.id;

            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = this.configuration["MensajeProductSuccess:AddErrorMessage"];
                this.logger.LogError(result.Message, ex.ToString());

            }
            return result;


        }

        public ServicesResult Update(ProductsDtoUpdate dtoUpdate)
        {
            ProductResponse result = new ProductResponse();

            // validaciones reutilizadas //
            ServicesResult validation = ValidateProductCommon(dtoUpdate);
            if (!validation.Success)
            {
                result.Message = validation.Message;
                result.Success = false;
                return result;
            }
            try
            {


                Producto product = new Producto()
                {
                    id = dtoUpdate.ProductId,
                    FechaRegistro = dtoUpdate.CreateDate,
                    IdUsuarioCreacion = dtoUpdate.ProductId,
                    Marca = dtoUpdate.Marca,
                    Descripcion = dtoUpdate.Description,
                    FechaMod = dtoUpdate.FechaMod,
                    IdUsuarioMod = dtoUpdate.IdUsuarioMod,
                    
                };
                this.productRepository.Update(product);
                result.Message = this.configuration["MensajeProductSuccess:UpdateSuccessMessage"];


            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = this.configuration["MensajeProductSuccess:UpdateErrorMessage"];
                this.logger.LogError(result.Message, ex.ToString());

            }
            return result;
        }

        public object GetProductByProductID(int productID)
        {
            throw new NotImplementedException();
        }
    }
}
