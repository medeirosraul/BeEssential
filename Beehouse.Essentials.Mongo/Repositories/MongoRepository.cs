using Beehouse.Essentials.Mongo.Context;
using Beehouse.Essentials.Mongo.Entities;
using Beehouse.Essentials.Types;
using Beehouse.Essentials.Types.Extensions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Mongo.Repositories
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : MongoEntity
    {
        private readonly MongoContext _context;
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(MongoContext context)
        {
            _context = context;
            _collection = _context.Collection<TDocument>();
        }

        public IMongoQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public IMongoQueryable<TDocument> AsQueryable(bool tracking)
        {
            return _collection.AsQueryable();
        }

        public async Task<bool> Exists(string id)
        {
            var query = AsQueryable();
            return await query.AnyAsync(p => p.Id == id && !p.Deleted);
        }

        public async Task<Paged<TDocument>> Get()
        {
            return await Get(AsQueryable());
        }

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

            // Apply some filters
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

        public async Task<TDocument> Insert(TDocument entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }
        public async Task<TDocument> GetById(string id)
        {
            return await AsQueryable().FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);
        }

        public async Task<TDocument> Update(TDocument entity)
        {
            var filter = Builders<TDocument>.Filter.Eq(p => p.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity);

            return entity;
        }

        public async Task Delete(string id)
        {
            await Delete(id, true);
        }

        public async Task Delete(string id, bool logic)
        {
            var document = await GetById(id);
            document.Deleted = true;
            await Update(document);
        }
    }
}
