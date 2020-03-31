using Beehouse.Essentials.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Core.Events;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Mongo.Context
{
    public class MongoContext
    {
        private static bool _firstRun = true;
        private readonly BeehouseMongoOptions _options;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoContext(IOptionsMonitor<BeehouseMongoOptions> optionsAccessor)
        {
            // Register type especific serialization
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));

            // Context instance
            _options = optionsAccessor.CurrentValue;
            var mongoConnectionUrl = new MongoUrl(_options.MongoConnectionString);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
            mongoClientSettings.ClusterConfigurator = cb => {
                cb.Subscribe<CommandStartedEvent>(e => {
                    Debug.WriteLine($"{e.CommandName} - {e.Command.ToJson()}");
                });
            };
            _client = new MongoClient(mongoClientSettings);
            _database = _client.GetDatabase(_options.DatabaseName);

            if (_firstRun)
            {
                CreateIndexes();
                _firstRun = false;
            }
        }

        public IMongoCollection<TDocument> Collection<TDocument>(string name) where TDocument : BaseEntity
        {
            return _database.GetCollection<TDocument>(name);
        }

        public IMongoCollection<TDocument> Collection<TDocument>() where TDocument : BaseEntity
        {
            // Try get name.
            var name = typeof(TDocument).GetCustomAttribute<BsonDiscriminatorAttribute>()?.Discriminator;

            // If does not have name, get class name.
            if (string.IsNullOrWhiteSpace(name))
                name = typeof(TDocument).Name;

            return Collection<TDocument>(name);
        }

        private async Task CreateIndexes()
        {
            await _options.IndexCreation(this);
        }
    }
}