using Beehouse.Essentials.BeAuth.Entities.Subscriptions;
using Beehouse.Essentials.BeAuthMongo.Entities;
using Beehouse.Essentials.BeAuthMongo.Repositories;
using Beehouse.Essentials.Mongo.Repositories;
using Beehouse.Essentials.Mongo.Services;
using Beehouse.Essentials.Services;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Beehouse.Essentials.BeAuthMongo.Services
{
    public class Service<TIdentifiableEntity>
        : MongoService<TIdentifiableEntity>, IService<TIdentifiableEntity>
        where TIdentifiableEntity:IdentifiableEntity
    {
        private readonly HttpContext _httpContext;
        private EntityIdentity _identity;

        public Service(IHttpContextAccessor httpContextAccessor, IRepository<TIdentifiableEntity> repository):base(repository)
        {
            _httpContext = httpContextAccessor.HttpContext;

            // Get user logged in.
            var user = GetBeAuthIdentity();

            // Create Entity Identity.
            _identity = new EntityIdentity
            {
                CreatedBy = user.FindFirst(SubscriptionClaimTypes.User).Value,
                OwnedBy = user.FindFirst(SubscriptionClaimTypes.Owner).Value
            };
        }

        public override async Task<TIdentifiableEntity> Insert(TIdentifiableEntity entity)
        {
            entity.Identity = _identity;
            return await base.Insert(entity);
        }

        private void Stamp(IdentifiableEntity entity)
        {

        }

        private ClaimsIdentity GetBeAuthIdentity()
        {
            var user = _httpContext.User.Identities.FirstOrDefault(p => p.AuthenticationType == "BeAuthIdentity");
            if (user == null) throw new NullReferenceException("No BeAuth valid identity provided.");

            return user;
        }
    }
}