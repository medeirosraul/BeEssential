using Beehouse.Essentials.Entities;
using Beehouse.Essentials.Repositories;
using Beehouse.Essentials.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Services
{
    public class ServiceBase<TEntity, TRepository, TQueryable> : IServiceBase<TEntity, TRepository, TQueryable>
        where TEntity : Entity
        where TQueryable : IQueryable<TEntity>
        where TRepository: IRepository<TQueryable, TEntity>
    {
        protected readonly TRepository Repository;

        public ServiceBase(TRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<bool> Exists(string id) => await Repository.Exists(id);

        public virtual async Task<TEntity> GetById(string id) => await Repository.GetById(id);

        public virtual async Task Delete(string id, bool logic = false) => await Repository.Delete(id, logic);

        public virtual async Task<ListResult<TEntity>> Get() => await Repository.Get();

        public virtual async Task<ListResult<TEntity>> Get(TQueryable query) => await Repository.Get(query);

        public virtual async Task<ListResult<TEntity>> Get(int page, int limit, TQueryable query) => await Repository.Get(page, limit, query);
       
        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.ModifiedAt = DateTime.Now;
            return await Repository.Insert(entity);
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            entity.ModifiedAt = DateTime.Now;
            return await Repository.Update(entity);
        }
    }
}
