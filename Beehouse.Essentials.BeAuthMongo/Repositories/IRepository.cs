using Beehouse.Essentials.BeAuthMongo.Entities;
using Beehouse.Essentials.Mongo.Repositories;

namespace Beehouse.Essentials.BeAuthMongo.Repositories
{
    public interface IRepository<TIdentifiableEntity> : IMongoRepository<TIdentifiableEntity> where TIdentifiableEntity : Identifiable
    {

    }
}
