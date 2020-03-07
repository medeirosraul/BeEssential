using Beehouse.Essentials.Entities;
using Beehouse.Essentials.Repositories;
using Beehouse.Essentials.Types;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Services
{
    public class BaseService<TEntity, TRepository, TQueryable> : IBaseService<TEntity, TRepository, TQueryable>
        where TEntity : BaseEntity
        where TQueryable : IQueryable<TEntity>
        where TRepository: IBaseRepository<TQueryable, TEntity>
    {
        protected readonly TRepository Repository;

        public BaseService(TRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<bool> Exists(string id) => await Repository.Exists(id);

        public virtual async Task<TEntity> GetById(string id) => await Repository.GetById(id);

        public virtual async Task Delete(string id, bool logic = false) => await Repository.Delete(id, logic);

        public virtual async Task<Paged<TEntity>> Get() => await Repository.Get();

        public virtual async Task<Paged<TEntity>> Get(TQueryable query) => await Repository.Get(query);

        public virtual async Task<Paged<TEntity>> Get(int page, int limit, TQueryable query) => await Repository.Get(page, limit, query);
       
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
