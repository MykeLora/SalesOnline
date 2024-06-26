﻿using Microsoft.Extensions.Logging;
using Sales.Application.Contract;
using Sales.Application.Core;
using Sales.Application.Dtos.Category;
using Sales.Application.Dtos.TDocumentVenta;
using Sales.Application.Models.TDocumentVentas;
using Sales.Domain.Entites; // Corregido el namespace
using Sales.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Service
{
    public class TDocumentServiceNew : ITDocumentVentService
    {
        private readonly ILogger<TDocumentServiceNew> logger;
        private readonly ITipoDocumentoVentaRepository tipoDocumentoVentaRepository;

        public TDocumentServiceNew(ILogger<TDocumentServiceNew> logger,
                              ITipoDocumentoVentaRepository tipoDocumentoVentaRepository) // Corregido el nombre del parámetro
        {
            this.logger = logger;
            this.tipoDocumentoVentaRepository = tipoDocumentoVentaRepository; // Corregido el nombre de la variable
        }

        public ServicesResult<TDocumentVentaGetModel> Get(int Id)
        {
            ServicesResult< TDocumentVentaGetModel> result = new ServicesResult<TDocumentVentaGetModel>();

            try
            {
                var tdocument = this.tipoDocumentoVentaRepository.GetEntity(Id);

                if (tdocument != null)
                {
                    result.Data = new TDocumentVentaGetModel()
                    {
                        TDocumentVentaId = tdocument.id,
                        Descripcion = tdocument.Descripcion,
                        EsActivo = tdocument.EsActivo,
                        CreateDate = tdocument.FechaRegistro
                    };
                }
                else
                {
                    result.Success = false;
                    result.Message = "El tipo de documento de venta no existe.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener el documento de venta.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult<List<TDocumentVentaGetModel>> GetAll()
        {
            ServicesResult<List<TDocumentVentaGetModel>> result = new ServicesResult<List<TDocumentVentaGetModel>>();

            try
            {
                var tdocuments = this.tipoDocumentoVentaRepository.GetEntities().Select(
                    document => new TDocumentVentaGetModel()
                    {
                        TDocumentVentaId = document.id,
                        Descripcion = document.Descripcion,
                        EsActivo = document.EsActivo,
                        CreateDate = document.FechaRegistro
                    }).ToList();

                result.Data = tdocuments;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener los documentos de ventas.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult<TDocumentVentaGetModel> Remove(TDocumentRemoveDto RemoveDto)
        {
            ServicesResult<TDocumentVentaGetModel> result = new ServicesResult<TDocumentVentaGetModel>();

            try
            {
                this.tipoDocumentoVentaRepository.Remove(new TipoDocumentoVenta()
                {
                    id = RemoveDto.TdocumentId,
                    IdUsuarioElimino = RemoveDto.UserId,
                    FechaElimino = RemoveDto.ChangeDate
                });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al eliminar el documento de la venta.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult<TDocumentVentaGetModel> Save(TDocumentDtoAdd AddDto)
        {
            ServicesResult<TDocumentVentaGetModel> result = new ServicesResult<TDocumentVentaGetModel>();

            try
            {
                var validationResult = this.IsValid(AddDto);

                if (!validationResult.Success)
                {
                    result.Message = validationResult.Message;
                    return result;
                }

                this.tipoDocumentoVentaRepository.Save(new TipoDocumentoVenta() 
                {
                    Descripcion = AddDto.Descripcion,
                    IdUsuarioCreacion = AddDto.UserId,
                    EsActivo = AddDto.esActivo,
                    FechaRegistro = AddDto.ChangeDate
                });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al guardar el documento de venta.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult<TDocumentVentaGetModel> Update(TDocumentDtoUpdate UpdteDto)
        {
            ServicesResult<TDocumentVentaGetModel> result = new ServicesResult<TDocumentVentaGetModel>();

            try
            {
                var validationResult = this.IsValid(UpdteDto);

                if (!validationResult.Success)
                {
                    result.Message = validationResult.Message;
                    return result;
                }

                var document = this.tipoDocumentoVentaRepository.GetEntity(UpdteDto.TdocumentId);

                if (document == null)
                {
                    result.Success = false;
                    result.Message = "El documento de la venta  no existe.";
                    return result;
                }

                document.Descripcion = UpdteDto.Descripcion;
                document.EsActivo = UpdteDto.esActivo;
                document.FechaMod = UpdteDto.ChangeDate;
                document.IdUsuarioMod = UpdteDto.UserId;

                this.tipoDocumentoVentaRepository.Update(document);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al actualizar el documento de la venta.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;

        }

        private ServicesResult<string> IsValid(TDocumentDtoBase tdocumenDtoBase)
        {
            ServicesResult<string> result = new ServicesResult<string>();

            if (string.IsNullOrEmpty(tdocumenDtoBase.Descripcion))
            {
                result.Success = false;
                result.Message = "La descripcion es requerida.";
                return result;
            }

            if (tdocumenDtoBase.Descripcion.Length > 200)
            {
                result.Success = false;
                result.Message = "La descripcion de el documento de venta debe tener máximo 15 caracteres.";
                return result;
            }

            return result;
        }
    }
}
