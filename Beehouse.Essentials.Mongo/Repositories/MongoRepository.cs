using Beehouse.Essentials.Mongo.Context;
using Beehouse.Essentials.Mongo.Entities;
using Beehouse.Essentials.Types;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Mongo.Repositories
{
    /// <summary>
    /// Mongo Repository implementation
    /// </summary>
    /// <typeparam name="TDocument">MongoEntity</typeparam>
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : MongoEntity
    {
        private readonly MongoContext _context;
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(MongoContext context)
        {
            _context = context;
            _collection = _context.Collection<TDocument>();
        }

        /// <summary>
        /// Return a queryable object
        /// </summary>
        /// <returns>Queryable</returns>
        public IMongoQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        /// <summary>
        /// Return a queryable object
        /// </summary>
        /// <param name="tracking">As tracking</param>
        /// <returns>Queryable</returns>
        public IMongoQueryable<TDocument> AsQueryable(bool tracking)
        {
            return AsQueryable();
        }

        /// <summary>
        /// Return if entity exists in database
        /// </summary>
        /// <param name="id">Entity identification</param>
        /// <returns>True or false if exists</returns>
        public async Task<bool> Exists(string id)
        {
            var query = AsQueryable();
            return await query.AnyAsync(p => p.Id == id && !p.Deleted);
        }

        /// <summary>
        /// Get a paged list of entities
        /// </summary>
        /// <returns>Paged list of entities</returns>
        public async Task<Paged<TDocument>> Get()
        {
            return await Get(AsQueryable());
        }

        /// <summary>
        /// Get a paged list of entities with a query
        /// </summary>
        /// <param name="query">Query filter</param>
        /// <returns>Paged list of entities</returns>
        public async Task<Paged<TDocument>> Get(IMongoQueryable<TDocument> query)
        {
            return await Get(1, 0, query);
        }

        /// <summary>
        /// Return an object that contains paged result for a query
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="limit">Limit per page</param>
        /// <param name="query">Query to apply</param>
        /// <returns></returns>
        public async Task<Paged<TDocument>> Get(int page, int limit, IMongoQueryable<TDocument> query)
        {
            // Define a query
            query ??= AsQueryable();

            // Apply main filters
            query = query.Where(p => !p.Deleted);

            // Get count
            var total = await query.CountAsync();

            // Create result
            var result = new Paged<TDocument>
            {
                Total = total,
                Page = page,
                Limit = limit
            };

            // Return result
            if (total == 0) return result;

            query = limit == 0 ? query : query.Skip((page - 1) * limit).Take(limit);
            result.AddRange(await query.ToListAsync());

            return result;
        }

        /// <summary>
        /// Insert a entity in database
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <returns>Inserted entity</returns>
        public async Task<TDocument> Insert(TDocument entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        /// <summary>
        /// Get a entity by identification
        /// </summary>
        /// <param name="id">Entity identification</param>
        /// <returns>Entity</returns>
        public async Task<TDocument> GetById(string id)
        {
            return await AsQueryable().FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);
        }

        /// <summary>
        /// Update a entity in database
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>Updated entity</returns>
        public async Task<TDocument> Update(TDocument entity)
        {
            var filter = Builders<TDocument>.Filter.Eq(p => p.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity);

            return entity;
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Entity identification</param>
        public async Task Delete(string id)
        {
            await Delete(id, true);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Entity identification</param>
        /// <param name="logic">Logical delete</param>
        public async Task Delete(string id, bool logic)
        {
            var document = await GetById(id);
            document.Deleted = true;
            await Update(document);
        }

        public async Task FindOneAndUpdate(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update)
        {
            await _collection.FindOneAndUpdateAsync(filter, update);
        }
    }
}
