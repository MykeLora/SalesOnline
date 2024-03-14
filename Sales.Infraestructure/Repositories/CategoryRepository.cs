using Microsoft.Extensions.Logging;
using Sales.Domain.Entites;
using Sales.Infraestructure.Context;
using Sales.Infraestructure.Core;
using Sales.Infraestructure.Exceptions;
using Sales.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infraestructure.Repositories
{
    public class CategoryRepository : BaseRepository<Categoria>, ICategoryRepository
    {
        private readonly SalesContext context;
        private readonly ILogger<CategoryRepository> logger;

        public CategoryRepository(SalesContext context, ILogger<CategoryRepository> logger) : base(context)
        {
            this.context = context;
            this.logger = logger;
        }

        public virtual List<Categoria> GetEntities()
        {
            return base.GetEntities().Where(ca => !ca.Eliminado).ToList();
        }

        public override void Update(Categoria entity)
        {
            try
            {
                var categoryToUpdate = this.GetEntity(entity.id);

                if (categoryToUpdate == null)
                {
                    throw new CategoryException("La categoría no existe");
                }

                categoryToUpdate.nombre = entity.nombre;
                categoryToUpdate.IdUsuarioMod = entity.IdUsuarioMod;
                categoryToUpdate.Descripcion = entity.Descripcion;
                categoryToUpdate.FechaMod = entity.FechaMod;
                categoryToUpdate.EsActivo = entity.EsActivo;


                this.context.Categoria.Update(categoryToUpdate);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error al actualizar la categoría", ex.ToString());
            }
        }

        public override void Save(Categoria entity)
        {
            try
            {
                if (context.Categoria.Any(c => c.id == entity.id))
                {
                    this.logger.LogWarning("La categoría ya se encuentra registrada");
                }

                context.Categoria.Add(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error al registrar la categoría", ex.ToString());
            }
        }
        public override Categoria GetEntity(int id)
        {
            // Buscar la categoría en la base de datos
            var categoria = this.context.Categoria.Find(id);

            // Verificar si la categoría existe en la base de datos
            if (categoria != null)
            {
                return categoria; // Devolver la categoría encontrada
            }
            else
            {
                return null; // Devolver null si la categoría no existe
            }
        }


        public override bool Exists(Func<Categoria, bool> filter)
        {
            return base.Exists(filter);
        }

        public override List<Categoria> FinAll(Func<Categoria, bool> filter)
        {
            return base.FinAll(filter);
        }

        public virtual void Remove(Categoria entity)
        {
            try
            {
                Categoria categoryToRemove = this.GetEntity(entity.id);

                if (categoryToRemove is null)
                    throw new CategoryException("La categoria no existe.");

                categoryToRemove.FechaElimino = entity.FechaElimino;
                categoryToRemove.IdUsuarioElimino = entity.IdUsuarioElimino;
                categoryToRemove.Eliminado = true;

                this.context.Categoria.Update(categoryToRemove);
                this.context.SaveChanges();

            }
            catch (Exception ex)
            {
                this.logger.LogError("", ex.ToString());
            }
        }
    } 

}

