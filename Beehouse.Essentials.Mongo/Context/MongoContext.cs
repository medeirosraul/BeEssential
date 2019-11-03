using Beehouse.Essentials.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Beehouse.Essentials.Mongo.Context
{
    public class MongoContext
    {
        private readonly BeehouseMongoOptions _options;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoContext(IOptionsMonitor<BeehouseMongoOptions> optionsAccessor)
        {
            _options = optionsAccessor.CurrentValue;
            _client = new MongoClient(_options.MongoConnectionString);
            _database = _client.GetDatabase(_options.DatabaseName);
        }

        public IMongoCollection<TDocument> Collection<TDocument>(string name) where TDocument : Entity
        {
            return _database.GetCollection<TDocument>(name);
        }

        public IMongoCollection<TDocument> Collection<TDocument>() where TDocument : Entity
        {
            var name = typeof(TDocument).GetCustomAttribute<BsonDiscriminatorAttribute>()?.Discriminator;
            return Collection<TDocument>(name);
        }
    }
}