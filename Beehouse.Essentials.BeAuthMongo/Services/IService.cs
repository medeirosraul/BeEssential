using Beehouse.Essentials.BeAuthMongo.Entities;
using Beehouse.Essentials.Mongo.Repositories;
using Beehouse.Essentials.Services;
using MongoDB.Driver.Linq;

namespace Beehouse.Essentials.BeAuthMongo.Services
{
    public interface IService<TIdentifiable> : IBaseService<TIdentifiable, IMongoRepository<TIdentifiable>, IMongoQueryable<TIdentifiable>>
        where TIdentifiable : Identifiable
    {

    }
}