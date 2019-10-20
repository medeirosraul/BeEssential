using Beehouse.Essentials.BeAuth.Entities;
using Beehouse.Essentials.BeAuthMongo.Entities;
using Beehouse.Essentials.Mongo.Repositories;
using Beehouse.Essentials.Services;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver.Linq;

namespace Beehouse.Essentials.BeAuthMongo.Services
{
    public class IdentifiableService<TIdentifiableEntity> :
        ServiceBase<TIdentifiableEntity, IMongoRepository<TIdentifiableEntity>, IMongoQueryable<TIdentifiableEntity>> where TIdentifiableEntity:IdentifiableEntity
    {
        private readonly HttpContext _httpContext;
        private EntityIdentity _identity;

        public IdentifiableService(IHttpContextAccessor httpContextAccessor, IMongoRepository<TIdentifiableEntity> repository):base(repository)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }
    }
}
