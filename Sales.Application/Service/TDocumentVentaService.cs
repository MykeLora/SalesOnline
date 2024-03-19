using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sales.Application.Contract;
using Sales.Application.Core;
using Sales.Application.Dtos.TDocumentVenta;
using Sales.Application.Models.TDocumentVentas;
using Sales.Application.Reponse;
using Sales.Domain.Entites;
using Sales.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Service
{
    public class TDocumentVentaService : ITDocumentVentService
    { 
        private readonly ILogger<TDocumentVentaService> logger;
        private readonly ITipoDocumentoVentaRepository TdocumentRepository;
        private readonly IConfiguration configuration;

        public TDocumentVentaService(ILogger<TDocumentVentaService> logger,
                               ITipoDocumentoVentaRepository TdocumentRepository)
        {
            this.logger = logger;
            this.TdocumentRepository = TdocumentRepository;
            this.configuration = configuration;
        }


        // validaciones bases para ser reutilizadas por los metodos //
        private ServicesResult ValidateTDocumentCommon(TDocumentDtoBase dto)
        {
            ServicesResult result = new ServicesResult();


            if (string.IsNullOrEmpty(dto.Descripcion))
            {
                result.Success = false;
                result.Message = "El tipo de documento de venta es requerido.";
                return result;
            }
            if (dto.Descripcion.Length > 50)
            {
                result.Success = false;
                result.Message = "La descripcion del tipo de documento debe tener 50 carácteres minimo.";
                return result;
            }
            if (string.IsNullOrEmpty(dto.Descripcion))
            {
                result.Success = false;
                result.Message = "El tipo de documento de la venta es requerido es requerida.";
                return result;

            }

            if (this.TdocumentRepository.Exists(tp => tp.Descripcion == dto.Descripcion))
            {
                result.Success = false;
                result.Message = $"El documento de venta {dto.Descripcion} ya existe.";
                return result;
            }

            this.TdocumentRepository.Save(new Domain.Entites.TipoDocumentoVenta()
            {
                id = dto.IdTDocument,
                FechaRegistro = dto.ChangeDate,
                IdUsuarioCreacion = dto.IdUsuarioCreacion,
                Descripcion = dto.Description
            }) ;


            return result;
        }


        public ServicesResult GetAll()
        {
            ServicesResult result = new ServicesResult();

            try
            {
                var tdocument = this.TdocumentRepository.GetEntities().Select(
                    tdocument => new TDocumentDtoGetAll()
                    {
                        ChangeDate = tdocument.FechaRegistro,
                        ChanceUser = tdocument.IdUsuarioCreacion,
                        Description = tdocument.Descripcion

                    }).ToList();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener los document de venta";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult GetById(int id)
        {
            ServicesResult result = new ServicesResult();

            try
            {
                var tdocument = this.TdocumentRepository.GetEntity(id);

                result.Data = new TDocumentVentaGetModel()
                {
                    TDocumentVentaId= tdocument.id,
                    Descripcion = tdocument.Descripcion,
                    CreateDate = tdocument.FechaRegistro
                };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener el documento";
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }



        public ServicesResult Remove(TDocumentRemoveDto dtoRemove)
        {
            ServicesResult result = new ServicesResult();

            try
            {
                this.TdocumentRepository.Remove(new Domain.Entites.TipoDocumentoVenta()
                {
                    id = dtoRemove.IdTDocument,
                    IdUsuarioElimino = dtoRemove.IdUsuarioElimino,
                    FechaElimino = dtoRemove.FechaElimino


                });



            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al eliminar el tipo de documnento de la venta";
                this.logger.LogError(result.Message, ex.ToString());

            }
            return result;
        }

        public ServicesResult Save(TDocumentDtoAdd dtoAdd)
        {
            TDocumentVentaResponse result = new TDocumentVentaResponse();

            ServicesResult validation = ValidateTDocumentCommon(dtoAdd);

            if (!validation.Success)
            {
                result.Message = validation.Message;
                result.Success = false;
                return result;
            }
            try
            {
                TipoDocumentoVenta tdocument = new TipoDocumentoVenta()
                {
                    Descripcion = dtoAdd.Descripcion,
                    IdUsuarioCreacion = dtoAdd.IdUsuarioCreacion,
                    id = dtoAdd.IdTDocument,
                    EsActivo = dtoAdd.esActivo

                };

                this.TdocumentRepository.Save(tdocument);
                result.Message = this.configuration["MensajesTDocumentSuccess:AddSuccessMessage"];
                result.TDocumentVentaId = tdocument.id;

            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = this.configuration["MensajeTDocumentSuccess:AddErrorMessage"];
                this.logger.LogError(result.Message, ex.ToString());

            }
            return result;


        }

        private ServicesResult ValidateTDocumentCommon(TDocumentDtoAdd dtoAdd)
        {
            throw new NotImplementedException();
        }

        public ServicesResult Update(TDocumentDtoUpdate dtoUpdate)
        {
            TDocumentVentaResponse result = new TDocumentVentaResponse();

    
            ServicesResult validation = ValidateTDocumentCommon(dtoUpdate);
            if (!validation.Success)
            {
                result.Message = validation.Message;
                result.Success = false;
                return result;
            }
            try
            {


                TipoDocumentoVenta  tdocument = new TipoDocumentoVenta()
                {
                    FechaRegistro = dtoUpdate.ChangeDate,
                    IdUsuarioCreacion = dtoUpdate.IdUsuarioCreacion,
                    Descripcion = dtoUpdate.Description,
                    FechaMod = dtoUpdate.FechaMod,
                    IdUsuarioMod = dtoUpdate.ChanceUser,
                    id = dtoUpdate.IdTDocument
                };
                this.TdocumentRepository.Update(tdocument);
                result.Message = this.configuration["MensajeTDocumentSuccess:UpdateSuccessMessage"];


            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = this.configuration["MensajeTDocumentSuccess:UpdateErrorMessage"];
                this.logger.LogError(result.Message, ex.ToString());

            }
            return result;
        }

        public object GetTDocumentByTDocumentID(int TDocumentID)
        {
            throw new NotImplementedException();
        }
    }
}

