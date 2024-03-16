using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sales.Application.Contract;
using Sales.Application.Core;
using Sales.Application.Dtos.Category;
using Sales.Application.Models.Category;
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
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> logger;
        private readonly ICategoryRepository categoryRepository;
        private readonly IConfiguration configuration;

        public CategoryService(ILogger<CategoryService>logger,
                               ICategoryRepository categoryRepository)
        {
            this.logger = logger;
            this.categoryRepository = categoryRepository;
            this.configuration = configuration;
        }


        // validaciones bases para ser reutilizadas por los metodos //
        private ServicesResult ValidateCategoryCommon(CategoryDtoBase dto)
        {
            ServicesResult result = new ServicesResult();


            if (string.IsNullOrEmpty(dto.Name))
            {
                result.Success = false;
                result.Message = "La categoria es requerida.";
                return result;
            }
            if (dto.Name.Length > 15)
            {
                result.Success = false;
                result.Message = "El nombre de la categoria debe tener 15 carácteres.";
                return result;
            }
            if (string.IsNullOrEmpty(dto.Description))
            {
                result.Success = false;
                result.Message = "La categoria es requerida.";
                return result;

            }
            if (dto.Description.Length > 200)
            {
                result.Success = false;
                result.Message = "La descripción de la categoria debe tener 200 carácteres.";
                return result;
            }

            if (this.categoryRepository.Exists(ca => ca.nombre == dto.Name))
            {
                result.Success = false;
                result.Message = $"La categoria {dto.Name} ya existe.";
                return result;
            }

            this.categoryRepository.Save(new Domain.Entites.Categoria()
            {
                nombre = dto.Name,
                FechaRegistro = dto.ChangeDate,
                IdUsuarioCreacion = dto.IdUsuarioCreacion,
                Descripcion = dto.Description
            });


            return result;
        }


        public ServicesResult GetAll()
        {
            ServicesResult result = new ServicesResult();

            try
            {
                var categories = this.categoryRepository.GetEntities().Select(
                    categories => new CategoryDtoGetAll()
                    {
                        ChangeDate = categories.FechaRegistro,
                        ChanceUser = categories.IdUsuarioCreacion,
                        Description = categories.Descripcion,
                        Name = categories.nombre

                    }).ToList();
            }
            catch( Exception ex )
            {
                result.Success = false;
                result.Message = "Error al obtener las categorias";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult GetById(int id)
        {
            ServicesResult result = new ServicesResult();

            try
            {
                var categories = this.categoryRepository.GetEntity(id);

                result.Data = new CategoryGetModel()
                {
                    CategoryId =  categories.id,
                    Description = categories.Descripcion,
                    Name = categories.nombre,
                    CreateDate = categories.FechaRegistro
                };
            }
            catch ( Exception ex )
            {
                result.Success = false;
                result.Message = "Error al obtener la categoria";
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public object GetCategoryByCategoryID(int categoryID)
        {
            throw new NotImplementedException();
        }

        public ServicesResult Remove(CategoryRemoveDto dtoRemove)
        {
            ServicesResult result = new ServicesResult();

            try
            {
                this.categoryRepository.Remove(new Categoria()
                {
                    id = dtoRemove.CategoryId,
                    IdUsuarioElimino = dtoRemove.IdUsuarioElimino,
                    FechaElimino = dtoRemove.FechaElimino
                    

                });



            }catch( Exception ex )
            {
                result.Success = false;
                result.Message = "Error al eliminar la categoria";
                this.logger.LogError(result.Message, ex.ToString());

            }
            return result;
        }

        public ServicesResult Save(CategoryDtoAdd dtoAdd)
        {
            CategoryResponse result = new CategoryResponse();

            ServicesResult validation = ValidateCategoryCommon(dtoAdd);

            if (!validation.Success)
            {
                result.Message = validation.Message;
                result.Success = false;
                return result;
            }
            try
            {
                Categoria category = new Categoria()
                {
                    Descripcion = dtoAdd.Description,
                    IdUsuarioCreacion = dtoAdd.IdUsuarioCreacion,
                    nombre= dtoAdd.Name,
                    id = dtoAdd.CategoryId


                };

                this.categoryRepository.Save(category);
                result.Message = this.configuration["MensajesCategoriaSuccess:AddSuccessMessage"];
                result.CategoryId = category.id;

            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = this.configuration["MensajeCategoriaSuccess:AddErrorMessage"];
                this.logger.LogError(result.Message, ex.ToString());

            }
            return result;


        }

        public ServicesResult Update(CategoryDtoUpdate dtoUpdate)
        {
            CategoryResponse result = new CategoryResponse();

            // validaciones reutilizadas //
            ServicesResult validation = ValidateCategoryCommon(dtoUpdate);
            if (!validation.Success)
            {
                result.Message = validation.Message;
                result.Success = false;
                return result;
            }
            try
            {


                Categoria category = new Categoria()
                {
                    FechaRegistro = dtoUpdate.ChangeDate,
                    IdUsuarioCreacion = dtoUpdate.IdUsuarioCreacion,
                    nombre = dtoUpdate.Name,
                    Descripcion = dtoUpdate.Description,
                    FechaMod = dtoUpdate.FechaMod,
                    IdUsuarioMod = dtoUpdate.ChanceUser,
                    id = dtoUpdate.CategoryId
                };
                this.categoryRepository.Update(category);
                result.Message = this.configuration["MensajeCategoriaSuccess:UpdateSuccessMessage"];


            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = this.configuration["MensajeCategoriaSuccess:UpdateErrorMessage"];
                this.logger.LogError(result.Message, ex.ToString());

            }
            return result;
        }
    }
}
