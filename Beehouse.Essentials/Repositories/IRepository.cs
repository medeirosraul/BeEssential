﻿using Beehouse.Essentials.Entities;
using Beehouse.Essentials.Util;
using System.Linq;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Repositories
{
    public interface IRepository<TQueryable, TEntity>
        where TQueryable:IQueryable<TEntity>
        where TEntity:Entity
    {
        TQueryable AsQueryable();
        TQueryable AsQueryable(bool tracking);
        Task<bool> Exists(string id);
        Task<TEntity> GetById(string id);
        Task<ListResult<TEntity>> Get();
        Task<ListResult<TEntity>> Get(TQueryable query);
        Task<ListResult<TEntity>> Get(int page, int limit, TQueryable query);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(string id, bool logic = false);
    }
}
