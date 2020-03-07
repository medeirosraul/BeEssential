using Beehouse.Essentials.BeAuthMongo.Entities;
using Beehouse.Essentials.Mongo.Repositories;

namespace Beehouse.Essentials.BeAuthMongo.Repositories
{
    public class Repository<TIdentifiable> : MongoRepository<TIdentifiable>, IRepository<TIdentifiable> where TIdentifiable : Identifiable
    {
        public Repository(Mongo.Context.MongoContext context) : base(context)
        {
        }
    }
}
