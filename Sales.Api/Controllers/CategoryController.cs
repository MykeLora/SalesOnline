using Microsoft.AspNetCore.Mvc;
using Sales.Api.Dtos.Categoria;
using Sales.Api.Models;
using Sales.Domain.Entites;
using Sales.Infraestructure.Interfaces;


namespace shopping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("GetCategories")]
        public IActionResult Get()
        {
            var categories = this.categoryRepository.GetEntities().Select(cd => new CategoryAddModel()
            {
                CategoryId = cd.id,
                CreateDate = cd.FechaRegistro,
                Description = cd.Descripcion,
                Name = cd.nombre

            });

            return Ok(categories);

        }


        [HttpGet("GetCategoryById")]
        public IActionResult Get(int id)
        {
            var category = this.categoryRepository.GetEntity(id);

            CategoryAddModel categoryGetModel = new CategoryAddModel()
            {
                CategoryId = category.id,
                CreateDate = category.FechaRegistro,
                Description = category.Descripcion,
                Name = category.nombre
            };

            return Ok(categoryGetModel);
        }

        [HttpPost("SaveCategory")]
        public IActionResult Post([FromBody] CategoriaAddDto categoryAddModel)
        {
            this.categoryRepository.Save(new Sales.Domain.Entites.Categoria()
            {
                nombre = categoryAddModel.Name,
                FechaRegistro = categoryAddModel.ChangeDate,
                IdUsuarioCreacion = categoryAddModel.UserId,
                Descripcion = categoryAddModel.Description
            });

            return Ok("Categoria guardada correctamente.");

        }


        [HttpPut("UpdateCategory")]
        public IActionResult Put([FromBody] CategoriaUpdateDto categoryUpdte)
        {
            this.categoryRepository.Update(new Categoria()
            {
                id = categoryUpdte.CategoryId,
                nombre = categoryUpdte.Name,
                FechaMod = categoryUpdte.ChangeDate,
                IdUsuarioMod = categoryUpdte.UserId,
                Descripcion = categoryUpdte.Description,
            });

            return Ok("Categoria actualizada correctamente.");
        }


        [HttpDelete("RemoveCategory")]
        public IActionResult Remove([FromBody] CategoriaRemoveDto categoryRemove)
        {

            this.categoryRepository.Remove(new Categoria()
            {
                id = categoryRemove.CategoryId,
                FechaElimino = categoryRemove.ChangeDate,
                IdUsuarioElimino = categoryRemove.UserId
            });

            return Ok("Categoria eliminada correctamente.");
        }
    }
}