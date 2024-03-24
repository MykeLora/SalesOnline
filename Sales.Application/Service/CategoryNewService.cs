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

        public ServicesResult<CategoryDtoGetAll> Get(int Id)
        {
            ServicesResult<CategoryDtoGetAll> result = new ServicesResult<CategoryDtoGetAll>();

            try
            {
                var category = this.categoryRepository.GetEntity(Id);

                if (category != null)
                {
                    result.Data = new CategoryDtoGetAll()
                    {
                        CategoryId = category.id,
                        Description = category.Descripcion,
                        Name = category.nombre,
                        ChangeDate = category.FechaRegistro
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

        public ServicesResult<List<CategoryDtoGetAll>> GetAll()
        {
            ServicesResult<List<CategoryDtoGetAll>> result = new ServicesResult<List<CategoryDtoGetAll>>();

            try
            {
                var categories = this.categoryRepository.GetEntities().Select(
                    category => new CategoryDtoGetAll()
                    {
                        CategoryId = category.id,
                        Description = category.Descripcion,
                        Name = category.nombre,
                        ChangeDate = category.FechaRegistro
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

        public ServicesResult<CategoryDtoGetAll> Remove(CategoryRemoveDto RemoveDto)
        {
            ServicesResult<CategoryDtoGetAll> result = new ServicesResult<CategoryDtoGetAll>();

            try
            {
                this.categoryRepository.Remove(new Categoria()
                {
                    id = RemoveDto.CategoryId,
                    IdUsuarioElimino = RemoveDto.IdUsuarioElimino,
                    FechaElimino = RemoveDto.FechaElimino
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

        public ServicesResult<CategoryDtoGetAll> Save(CategoryDtoAdd AddDto)
        {
            ServicesResult<CategoryDtoGetAll> result = new ServicesResult<CategoryDtoGetAll>();

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
                    IdUsuarioCreacion = AddDto.IdUsuarioCreacion,
                    nombre = AddDto.Name,
                    id = AddDto.CategoryId
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

        public ServicesResult<CategoryDtoGetAll> Update(CategoryDtoUpdate UpdateDto)
        {
            ServicesResult<CategoryDtoGetAll> result = new ServicesResult<CategoryDtoGetAll>();

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
                category.IdUsuarioMod = UpdateDto.ChanceUser;

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
