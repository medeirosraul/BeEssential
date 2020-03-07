using Beehouse.Essentials.Entities;
using Beehouse.Essentials.Types;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Services
{
    public interface IBaseService<TEntity, TRepository, TQueryable> where TEntity : BaseEntity
    {
        Task<bool> Exists(string id);
        Task<TEntity> GetById(string id);
        Task<Paged<TEntity>> Get();
        Task<Paged<TEntity>> Get(TQueryable query);
        Task<Paged<TEntity>> Get(int page, int limit, TQueryable query);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(string id, bool logic = false);
    }
}
