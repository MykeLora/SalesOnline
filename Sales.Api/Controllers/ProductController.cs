using Microsoft.AspNetCore.Mvc;
using Sales.Api.Dtos.Categoria;
using Sales.Api.Dtos.Product;
using Sales.Api.Models;
using Sales.Domain.Entites;
using Sales.Infraestructure.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductoRepository productRepository;

        public ProductController(IProductoRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet("GetProducts")]
        public IActionResult Get()
        {
            var products = this.productRepository.GetEntities().Select(cd => new ProductAddModel()
            {
                ProductId = cd.id,
                CreateDate = cd.FechaRegistro,
                Description = cd.Descripcion,
                Name = cd.Marca,
                Price = cd.Precio,
                CategoryId = cd.id

            });

            return Ok(products);

        }


        [HttpGet("GetdProductById")]
        public IActionResult Get(int id)
        {
            var product = this.productRepository.GetEntity(id);

            ProductAddModel productGetModel = new ProductAddModel()
            {
                ProductId = product.id,
                CategoryId = product.id,
                CreateDate = product.FechaRegistro,
                Description = product.Descripcion,
                Name = product.Marca,
                Price = product.Precio,

            };

            return Ok(productGetModel);
        }

        [HttpPost("SaveProduct")]
        public IActionResult Post([FromBody] ProductAddDto productAddModel)
        {
            this.productRepository.Save(new Sales.Domain.Entites.Producto()
            {
                id = productAddModel.ProductId,
                Marca = productAddModel.Marca,
                Precio = productAddModel.Precio,
                FechaRegistro = productAddModel.ChangeDate,
                IdUsuarioCreacion = productAddModel.UserId,
                Descripcion = productAddModel.Descripcion,
                IdCategoria = productAddModel.IdCategoria,
            });

            return Ok("Producto guardado correctamente.");

        }


        [HttpPut("UpdateProduct")]
        public IActionResult Put([FromBody] ProductUpdateDto productUpdte)
        {
            this.productRepository.Update(new Producto()
            {
                id = productUpdte.ProductId,
                Marca = productUpdte.Marca,
                FechaMod = productUpdte.ChangeDate,
                IdUsuarioMod = productUpdte.UserId,
                Descripcion = productUpdte.Descripcion,
            });

            return Ok("Producto actualizado correctamente.");
        }


        [HttpDelete("RemoveProduct")]
        public IActionResult Remove([FromBody] ProductRemoveDto productRemove)
        {

            this.productRepository.Remove(new Producto()
            {
                id = productRemove.ProductId,
                FechaElimino = productRemove.ChangeDate,
                IdUsuarioElimino = productRemove.UserId
            });

            return Ok("Producto eliminado correctamente.");
        }
    }
}
