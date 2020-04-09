using Beehouse.Essentials.BeAuth.Entities.Subscriptions;
using Beehouse.Essentials.BeAuthMongo.Entities;
using Beehouse.Essentials.Mongo.Repositories;
using Beehouse.Essentials.Services;
using Beehouse.Essentials.Types;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Beehouse.Essentials.BeAuthMongo.Services
{
    public class Service<TIdentifiable> : BaseService<TIdentifiable, IMongoRepository<TIdentifiable>, IMongoQueryable<TIdentifiable>>, IService<TIdentifiable>
        where TIdentifiable : Identifiable
    {
        private readonly HttpContext _httpContext;
        private readonly EntityIdentity _identity;
        private readonly bool _isOwner;

        public Service(IHttpContextAccessor httpContextAccessor, IMongoRepository<TIdentifiable> repository):base(repository)
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

            // Check Owner
            _isOwner = _identity.CreatedBy == _identity.OwnedBy;
        }

        public override async Task<TIdentifiable> Insert(TIdentifiable entity)
        {
            entity.Identity = _identity;
            return await base.Insert(entity);
        }

        public override async Task<PagedList<TIdentifiable>> Get()
        {
            return await Get(null);
        }

        public override async Task<PagedList<TIdentifiable>> Get(IMongoQueryable<TIdentifiable> query)
        {
            query ??= Repository.AsQueryable();

            if (_isOwner) query = query.Where(p => p.Identity.OwnedBy == _identity.OwnedBy);
            else query = query.Where(p => p.Identity.CreatedBy == _identity.CreatedBy);

            return await base.Get(query);
        }

        public override async Task<PagedList<TIdentifiable>> Get(int page, int limit, IMongoQueryable<TIdentifiable> query)
        {
            query ??= Repository.AsQueryable();

            if (_isOwner) query = query.Where(p => p.Identity.OwnedBy == _identity.OwnedBy);
            else query = query.Where(p => p.Identity.CreatedBy == _identity.CreatedBy);

            return await base.Get(page, limit, query);
        }

        private void Stamp(Identifiable entity)
        {

        }

        private ClaimsIdentity GetBeAuthIdentity()
        {
            var user = _httpContext.User.Identities.FirstOrDefault(); //(p => p.AuthenticationType == "BeAuthIdentity");
            if (user == null) throw new NullReferenceException("No BeAuth valid identity provided.");

            return user;
        }
    }
}