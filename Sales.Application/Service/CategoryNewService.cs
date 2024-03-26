using Microsoft.Extensions.Logging;
using Sales.Application.Contract;
using Sales.Application.Core;
using Sales.Application.Dtos.Category;
using Sales.Application.Models.Category;
using Sales.Domain.Entites;
using Sales.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sales.Application.Service
{
    public class CategoryNewService : ICategoryService
    {
        private readonly ILogger<CategoryNewService> logger;
        private readonly ICategoryRepository categoryRepository;

        public CategoryNewService(ILogger<CategoryNewService> logger,
                               ICategoryRepository categoryRepository)
        {
            this.logger = logger;
            this.categoryRepository = categoryRepository;
        }

        public ServicesResult<CategoryGetModel> Get(int Id)
        {
            ServicesResult<CategoryGetModel> result = new ServicesResult<CategoryGetModel>();

            try
            {
                var category = this.categoryRepository.GetEntity(Id);

                if (category != null)
                {
                    result.Data = new CategoryGetModel
                    {
                        CategoryId = category.id,
                        Description = category.Descripcion,
                        Name = category.nombre,
                        CreationDate = category.FechaRegistro
                    };
                }
                else
                {
                    result.Success = false;
                    result.Message = "La categoría no existe.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener la categoría.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;

        }

        public ServicesResult<List<CategoryGetModel>> GetAll()
        {
            ServicesResult<List<CategoryGetModel>> result = new ServicesResult<List<CategoryGetModel>>();

            try
            {
                var categories = this.categoryRepository.GetEntities().Select(
                    category => new CategoryGetModel()
                    {
                        CategoryId = category.id,
                        Description = category.Descripcion,
                        Name = category.nombre,
                        CreationDate = category.FechaRegistro
                    }).ToList();

                result.Data = categories;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener las categorías.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult<CategoryGetModel> Remove(CategoryRemoveDto RemoveDto)
        {
            ServicesResult<CategoryGetModel> result = new ServicesResult<CategoryGetModel>();

            try
            {
                this.categoryRepository.Remove(new Categoria()
                {
                    id = RemoveDto.CategoryId,
                    IdUsuarioElimino = RemoveDto.UserId,
                    FechaElimino = RemoveDto.ChangeDate
                });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al eliminar la categoría.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult<CategoryGetModel> Save(CategoryDtoAdd AddDto)
        {
            ServicesResult<CategoryGetModel> result = new ServicesResult<CategoryGetModel>();

            try
            {
                var validationResult = this.IsValid(AddDto);

                if (!validationResult.Success)
                {
                    result.Message = validationResult.Message;
                    return result;
                }

                this.categoryRepository.Save(new Categoria()
                {
                    Descripcion = AddDto.Description,
                    IdUsuarioCreacion = AddDto.UserId,
                    nombre = AddDto.Name,
                    FechaRegistro = AddDto.ChangeDate
                });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al guardar la categoría.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public ServicesResult<CategoryGetModel> Update(CategoryDtoUpdate UpdateDto)
        {
            ServicesResult<CategoryGetModel> result = new ServicesResult<CategoryGetModel>();

            try
            {
                var validationResult = this.IsValid(UpdateDto);

                if (!validationResult.Success)
                {
                    result.Message = validationResult.Message;
                    return result;
                }

                var category = this.categoryRepository.GetEntity(UpdateDto.CategoryId);

                if (category == null)
                {
                    result.Success = false;
                    result.Message = "La categoría no existe.";
                    return result;
                }

                category.Descripcion = UpdateDto.Description;
                category.nombre = UpdateDto.Name;
                category.FechaMod = UpdateDto.ChangeDate;
                category.IdUsuarioMod = UpdateDto.UserId;

                this.categoryRepository.Update(category);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al actualizar la categoría.";
                this.logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        private ServicesResult<string> IsValid(CategoryDtoBase categoryDtoBase)
        {
            ServicesResult<string> result = new ServicesResult<string>();

            if (string.IsNullOrEmpty(categoryDtoBase.Name))
            {
                result.Success = false;
                result.Message = "El nombre de la categoría es requerido.";
                return result;
            }

            if (categoryDtoBase.Name.Length > 15)
            {
                result.Success = false;
                result.Message = "El nombre de la categoría debe tener máximo 15 caracteres.";
                return result;
            }

            if (string.IsNullOrEmpty(categoryDtoBase.Description))
            {
                result.Success = false;
                result.Message = "La descripción de la categoría es requerida.";
                return result;
            }

            if (categoryDtoBase.Description.Length > 200)
            {
                result.Success = false;
                result.Message = "La descripción de la categoría debe tener máximo 200 caracteres.";
                return result;
            }

            if (this.categoryRepository.Exists(ca => ca.nombre == categoryDtoBase.Name))
            {
                result.Success = false;
                result.Message = $"La categoría {categoryDtoBase.Name} ya existe.";
                return result;
            }

            return result;
        }
    }
}
