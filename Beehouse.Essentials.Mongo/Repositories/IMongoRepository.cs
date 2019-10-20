using System.Threading.Tasks;
using Beehouse.Essentials.Mongo.Entities;
using Beehouse.Essentials.Repositories;
using Beehouse.Essentials.Util;
using MongoDB.Driver.Linq;

namespace Beehouse.Essentials.Mongo.Repositories
{
    public interface IMongoRepository<TDocument>: IRepository<IMongoQueryable<TDocument>, TDocument> where TDocument : MongoEntity
    {

    }
}