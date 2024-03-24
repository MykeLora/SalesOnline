using Microsoft.AspNetCore.Mvc;
using Sales.Application.Contract;
using Sales.Application.Dtos.Product;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("GetProducts")]
        public IActionResult Get()
        {
            var result = this.productService.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result.Data);
            
        }

        // GET api/<ProductController>/5
        [HttpGet("GetProductById")]
        public IActionResult Get(int id)
        {
            var result = this.productService.Get(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result.Data);
            
        }

        // POST api/<ProductController>
        [HttpPost("SaveProduct")]
        public IActionResult Post([FromBody] ProductsDtoAdd productsDtoAdd)
        {
            var result = this.productService.Save(productsDtoAdd);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result.Data);
        }

        // PUT api/<ProductController>/5
        [HttpPut("SaveProduct")]
        public IActionResult Put( [FromBody] ProductsDtoUpdate productsDtoUpdate)
        {
            var result = this.productService.Update(productsDtoUpdate);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result.Data);

        }

        // DELETE api/<ProductController>/5
        [HttpDelete("RemoveProduct")]
        public IActionResult Delete(ProductsDtoRemove productsDtoRemove)
        {
            var result = this.productService.Remove(productsDtoRemove);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result.Data);
        }
    }
}
