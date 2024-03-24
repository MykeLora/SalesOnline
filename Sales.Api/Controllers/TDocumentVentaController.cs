using Microsoft.AspNetCore.Mvc;
using Sales.Api.Models.Modules.CategoryModule;
using Sales.Application.Contract;
using Sales.Application.Dtos.TDocumentVenta;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TDocumentVentaController : ControllerBase
    {


        private readonly ITDocumentVentService tDocumentVentService;

        public TDocumentVentaController(ITDocumentVentService documentVentService)
        {
            this.tDocumentVentService = documentVentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = this.tDocumentVentService.GetAll();

            if (result == null)
            {
                return NotFound("No documents found.");
            }

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result.Data);
        }
    

        [HttpGet("GetTDocumentById")]
        public IActionResult Get(int id)
        {
            var result = this.tDocumentVentService.Get(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result.Data);
        }

        // POST api/<TDocumentVentaController>
        [HttpPost("Save TDocumentVenta")]
        public IActionResult Post([FromBody] TDocumentDtoAdd documentDtoAdd)
        {
            var result = this.tDocumentVentService.Save(documentDtoAdd);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        // PUT api/<TDocumentVentaController>/5
        [HttpPut("UpdateTDocumentVenta")]
        public IActionResult Put([FromBody] TDocumentDtoUpdate documentDtoUpdate)
        {
            var result = this.tDocumentVentService.Update(documentDtoUpdate);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // DELETE api/<TDocumentVentaController>/5
        [HttpDelete("RemoveTDocumentVenta")]
        public IActionResult Delete(TDocumentRemoveDto documentRemoveDto)
        {
            var result = this.tDocumentVentService.Remove(documentRemoveDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }
    }
}
