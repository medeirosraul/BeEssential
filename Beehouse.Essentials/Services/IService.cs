using Beehouse.Essentials.Entities;
using Beehouse.Essentials.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Services
{
    public interface IService<TEntity> where TEntity : Entity
    {

        IQueryable<TEntity> GetEntities();
        IQueryable<TEntity> GetEntities(bool tracking);
        Task<bool> Exists(string id);
        Task<TEntity> GetById(string id);
        Task<ListResult<TEntity>> Get();
        Task<ListResult<TEntity>> Get(IQueryable<TEntity> query);
        Task<ListResult<TEntity>> Get(int page, int limit, IQueryable<TEntity> query);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(string id, bool logic = false);
        
    }
}
