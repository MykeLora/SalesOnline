using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sales.Domain.Entites;
using Sales.Domain.Repository;
using Sales.Infraestructure.Context;
using Sales.Infraestructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infraestructure.Core
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly SalesContext context;
        private readonly DbSet<TEntity> DbEntity;

        protected BaseRepository(SalesContext context)
        {
            this.context = context;
            this.DbEntity = context.Set<TEntity>();
        }

        public virtual bool Exists(Func<TEntity, bool> filter)
        {
            return DbEntity.Any(filter);
        }

        public virtual List<TEntity> FinAll(Func<TEntity, bool> filter)
        {
            return DbEntity.Where(filter).ToList();
        }


        public virtual List<TEntity> GetEntities()
        {
            // Filtra las entidades para excluir aquellas que contienen valores nulos
            return DbEntity.Where(entity => entity != null).ToList();
        }


        public virtual TEntity GetEntity(int id)
        {
            return DbEntity.Find(id);
        }

        public void Remove(TEntity entity)
        {
            DbEntity.Remove(entity);
            this.context.SaveChanges();
        }

        public virtual void Save(TEntity entity)
        {   
            DbEntity.Add(entity);
            context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            DbEntity.Update(entity);
            context.SaveChanges();
        }
    }
}
