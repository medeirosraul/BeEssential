using Beehouse.Essentials.Entities;
using Beehouse.Essentials.Mongo.Context;
using Beehouse.Essentials.Mongo.Repositories;
using Beehouse.Essentials.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.Mongo
{
    public static class BeehouseMongoExtensions
    {
        public static IServiceCollection AddBeehouseMongo(this IServiceCollection services, Action<BeehouseMongoOptions> options)
        {
            services.Configure(options);

            services.AddScoped(typeof (IRepository<,>), typeof (MongoRepository<>));
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddSingleton<MongoContext>();

            // Mappings
            BsonClassMap.RegisterClassMap<Entity>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.Id);
            });

            return services;
        }
    }
}