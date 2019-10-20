using Beehouse.Essentials.Mongo.Context;
using Beehouse.Essentials.Mongo.Entities;
using Beehouse.Essentials.Repositories;
using Beehouse.Essentials.Util;
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
            return await query.AnyAsync(p => p.Id == id);
        }

        public async Task<ListResult<TDocument>> Get()
        {
            var result = new ListResult<TDocument>
            {
                Page = 1,
                List = await AsQueryable().ToListAsync()
            };

            result.Count = result.List.Count;
            result.Limit = result.List.Count;

            return result;
        }

        public async Task<ListResult<TDocument>> Get(IMongoQueryable<TDocument> query)
        {
            if (query == null) query = AsQueryable();
            var result = new ListResult<TDocument>
            {
                Page = 1,
                List = await query.ToListAsync()
            };

            result.Count = result.List.Count;
            result.Limit = result.List.Count;

            return result;
        }

        public async Task<ListResult<TDocument>> Get(int page, int limit, IMongoQueryable<TDocument> query)
        {
            if (query == null) query = AsQueryable();
            return new ListResult<TDocument>
            {
                Page = page,
                Limit = limit,
                Count = await query.CountAsync(),
                List = await query.ToListAsync()
            };
        }

        public async Task<TDocument> Insert(TDocument entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }
        public async Task<TDocument> GetById(string id)
        {
            return await AsQueryable().FirstOrDefaultAsync(p => p.Id == id);
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
