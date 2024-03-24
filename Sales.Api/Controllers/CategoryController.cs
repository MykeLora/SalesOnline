using Microsoft.AspNetCore.Mvc;
using Sales.Application.Contract;
using Sales.Application.Dtos.Category;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("GetCategories")]
        public IActionResult Get()
        {
            var result = this.categoryService.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result.Data);
        }

        [HttpGet("GetCategoryById")]
        public IActionResult Get(int id)
        {
            var result = this.categoryService.Get(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result.Data);
        }

        [HttpPost("SaveCategory")]
        public IActionResult Post([FromBody] CategoryDtoAdd categoryAddModel)
        {
            var result = this.categoryService.Save(categoryAddModel);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("UpdateCategory")]
        public IActionResult Put([FromBody] CategoryDtoUpdate categoryUpdate)
        {
            var result = this.categoryService.Update(categoryUpdate);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("RemoveCategory")]
        public IActionResult Delete([FromBody] CategoryRemoveDto categoryRemove)
        {
            var result = this.categoryService.Remove(categoryRemove);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
