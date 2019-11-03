using Beehouse.Essentials.BeAuthMongo.Entities;
using Beehouse.Essentials.Mongo.Repositories;

namespace Beehouse.Essentials.BeAuthMongo.Repositories
{
    public class Repository<TIdentifiableEntity> : MongoRepository<TIdentifiableEntity>, IRepository<TIdentifiableEntity> where TIdentifiableEntity : IdentifiableEntity
    {
        public Repository(Mongo.Context.MongoContext context) : base(context)
        {
        }
    }
}
