using Beehouse.Essentials.BeAuthMongo.Interfaces;
using Beehouse.Essentials.Mongo.Entities;
using System.Collections.Generic;

namespace Beehouse.Essentials.BeAuthMongo.Entities
{
    public abstract class Identifiable : MongoEntity, IIdentified
    {
        public EntityIdentity Identity { get; set; }
        public ICollection<EntityStamp> Stamps { get; set; }
    }
}