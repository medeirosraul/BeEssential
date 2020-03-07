using Beehouse.Essentials.Mongo.Entities;
using Beehouse.Essentials.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Mongo.Repositories
{
    public interface IMongoRepository<TDocument>: IBaseRepository<IMongoQueryable<TDocument>, TDocument> where TDocument : MongoEntity
    {
        Task FindOneAndUpdate(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update);
    }
}