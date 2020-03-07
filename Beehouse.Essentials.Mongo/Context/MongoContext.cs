using Beehouse.Essentials.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
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
            _options = optionsAccessor.CurrentValue;
            _client = new MongoClient(_options.MongoConnectionString);
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