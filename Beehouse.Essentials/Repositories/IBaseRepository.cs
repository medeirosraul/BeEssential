using Beehouse.Essentials.Entities;
using Beehouse.Essentials.Types;
using System.Linq;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Repositories
{
    /// <summary>
    /// BaseRepository interface
    /// </summary>
    /// <typeparam name="TQueryable">IQueryable type</typeparam>
    /// <typeparam name="TEntity">BaseEntity type</typeparam>
    public interface IBaseRepository<TQueryable, TEntity>
        where TQueryable:IQueryable<TEntity>
        where TEntity:BaseEntity
    {
        /// <summary>
        /// Return set as queryable
        /// </summary>
        /// <returns>Queryable</returns>
        TQueryable AsQueryable();

        /// <summary>
        /// Return set as Queryable tracking or not
        /// </summary>
        /// <param name="tracking">Return as tracking?</param>
        /// <returns>Queryable</returns>
        TQueryable AsQueryable(bool tracking);

        /// <summary>
        /// Return if entity exists in database
        /// </summary>
        /// <param name="id">Entity identification</param>
        /// <returns>Exists</returns>
        Task<bool> Exists(string id);

        /// <summary>
        /// Return entity by Id
        /// </summary>
        /// <param name="id">Entity identification</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetById(string id);

        /// <summary>
        /// Get a list of entities
        /// </summary>
        /// <returns>Paged list of entities</returns>
        Task<PagedList<TEntity>> Get();

        /// <summary>
        /// Get a list of entities with query
        /// </summary>
        /// <param name="query">Query filter</param>
        /// <returns>Paged list of entities</returns>
        Task<PagedList<TEntity>> Get(TQueryable query);

        /// <summary>
        /// Get a list of entities with query and page and limit per page
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="limit">Limit per page</param>
        /// <param name="query">Query filter</param>
        /// <returns>Paged list of entities</returns>
        Task<PagedList<TEntity>> Get(int page, int limit, TQueryable query);

        /// <summary>
        /// Insert a entity in database
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <returns>Inserted entity</returns>
        Task<TEntity> Insert(TEntity entity);

        /// <summary>
        /// Update a entity in database
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>Updated Entity</returns>
        Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// Delete a entity in database
        /// </summary>
        /// <param name="id">Entity identification</param>
        /// <param name="logic">Logical delete</param>
        Task Delete(string id, bool logic = false);
    }
}
