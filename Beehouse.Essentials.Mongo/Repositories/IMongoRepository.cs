using Beehouse.Essentials.Mongo.Entities;
using Beehouse.Essentials.Repositories;
using MongoDB.Driver.Linq;

namespace Beehouse.Essentials.Mongo.Repositories
{
    public interface IMongoRepository<TDocument>: IRepositoryBase<IMongoQueryable<TDocument>, TDocument> where TDocument : MongoEntity
    {

    }
}