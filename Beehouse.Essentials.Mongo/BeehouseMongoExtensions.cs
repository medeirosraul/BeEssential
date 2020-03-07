using Beehouse.Essentials.Entities;
using Beehouse.Essentials.Mongo.Context;
using Beehouse.Essentials.Mongo.Repositories;
using Beehouse.Essentials.Repositories;
using Beehouse.Essentials.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System;

namespace Beehouse.Essentials.Mongo
{
    public static class BeehouseMongoExtensions
    {
        public static IServiceCollection AddBeehouseMongo(this IServiceCollection services, Action<BeehouseMongoOptions> options)
        {
            services.Configure(options);

            services.AddScoped(typeof (IBaseRepository<,>), typeof (MongoRepository<>));
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddSingleton<MongoContext>();

            // Mappings
            BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.Id);
            });

            return services;
        }
    }
}