using Beehouse.Essentials.Mongo.Entities;
using Beehouse.Essentials.Mongo.Repositories;
using Beehouse.Essentials.Services;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.Mongo.Services
{
    public interface IMongoService<TDocument> : IServiceBase<TDocument, IMongoRepository<TDocument>, IMongoQueryable<TDocument>>
        where TDocument : MongoEntity
    {
    }
}
