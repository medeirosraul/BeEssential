using Beehouse.Essentials.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Beehouse.Essentials.Mongo.Entities
{
    public abstract class MongoEntity : Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public override string Id { get; set; }

    }
}