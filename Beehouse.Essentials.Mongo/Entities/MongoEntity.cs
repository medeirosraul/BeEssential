using Beehouse.Essentials.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Beehouse.Essentials.Mongo.Entities
{
    /// <summary>
    /// Base Entity definition for Mongo documents
    /// </summary>
    public abstract class MongoEntity : BaseEntity
    {
        /// <summary>
        /// Entity identification
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public override string Id { get; set; }
    }
}