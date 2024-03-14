using Microsoft.AspNetCore.Mvc;
using Sales.Api.Dtos.Product;
using Sales.Api.Dtos.TDocumentVenta;
using Sales.Api.Models;
using Sales.Domain.Entites;
using Sales.Infraestructure.Interfaces;
using Sales.Infraestructure.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TDocumentVentaController : ControllerBase
    {
        private readonly ITipoDocumentoVentaRepository TipoDocumentoVentaRepository;

        public TDocumentVentaController(ITipoDocumentoVentaRepository TipoDocumentoVentaRepository)
        {
            this.TipoDocumentoVentaRepository = TipoDocumentoVentaRepository;
        }

        [HttpGet("GetDocumentsVenta")]
        public IActionResult Get()
        {
            var documents = this.TipoDocumentoVentaRepository.GetEntities().Select(cd => new TDocumentVentaAddModel()
            {
                TDocumentVentaId = cd.id,
                CreateDate = cd.FechaRegistro,
                Descripcion = cd.Descripcion,

            }); 

            return Ok(documents);

        }


        [HttpGet("GetDocumentVentaById")]
        public IActionResult Get(int id)
        {
            var documentoVenta = this.TipoDocumentoVentaRepository.GetEntity(id);

            TDocumentVentaAddModel documentGetModel = new TDocumentVentaAddModel()
            {
                TDocumentVentaId = documentoVenta.id,
                Descripcion = documentoVenta.Descripcion,
                CreateDate = documentoVenta.FechaRegistro,
                EsActivo = documentoVenta.EsActivo

            };

            return Ok(documentGetModel);
        }

        [HttpPost("SaveDocumentVentas")]
        public IActionResult Post([FromBody] TDocumentVentaAddDto tdocumentventaAddModel)
        {
            this.TipoDocumentoVentaRepository.Save(new Sales.Domain.Entites.TipoDocumentoVenta()
            {
                id = tdocumentventaAddModel.UserId,
                FechaRegistro = tdocumentventaAddModel.ChangeDate,
                IdUsuarioCreacion = tdocumentventaAddModel.UserId,
                Descripcion = tdocumentventaAddModel.Descripcion

            });

            return Ok("Tipo Documento de Venta guardado correctamente.");

        }


        [HttpPut("UpdateDocumentVenta")]
        public IActionResult Put([FromBody] TDocumentVentaUpdateDto documentUpdte)
        {
            this.TipoDocumentoVentaRepository.Update(new TipoDocumentoVenta()
            {
                id = documentUpdte.TDocumentId,
                FechaMod = documentUpdte.ChangeDate,
                IdUsuarioMod = documentUpdte.UserId,
                Descripcion = documentUpdte.Descripcion,
            });

            return Ok("Tipo Documento de Venta actualizado correctamente.");
        }


        [HttpDelete("RemoveProduct")]
        public IActionResult Remove([FromBody] TDocumentVentaRemoveDto documentRemove)
        {

            this.TipoDocumentoVentaRepository.Remove(new TipoDocumentoVenta()
            {
                id = documentRemove.TDocumentVentaId,
                FechaElimino = documentRemove.ChangeDate,
                IdUsuarioElimino = documentRemove.UserId
            });

            return Ok("Tipo Documento de Venta eliminado correctamente.");
        }
    }
}
