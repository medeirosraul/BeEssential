using Beehouse.Essentials.BeAuth.Entities;
using Beehouse.Essentials.BeAuth.Interfaces;
using Beehouse.Essentials.Mongo.Entities;

namespace Beehouse.Essentials.BeAuthMongo.Entities
{
    public abstract class IdentifiableEntity : MongoEntity, IIdentified
    {
        public EntityIdentity Identity { get; set; }
    }
}