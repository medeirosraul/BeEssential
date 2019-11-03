using Beehouse.Essentials.BeAuthMongo.Entities;
using Beehouse.Essentials.Mongo.Repositories;
using Beehouse.Essentials.Mongo.Services;
using Beehouse.Essentials.Services;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.BeAuthMongo.Services
{
    public interface IService<TIdentifiableEntity>
        : IMongoService<TIdentifiableEntity>
        where TIdentifiableEntity : IdentifiableEntity
    {

    }
}