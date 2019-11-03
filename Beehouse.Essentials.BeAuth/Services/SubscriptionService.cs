using Beehouse.Essentials.BeAuth.Data;
using Beehouse.Essentials.BeAuth.Entities.Identities;
using Beehouse.Essentials.BeAuth.Entities.Products;
using Beehouse.Essentials.BeAuth.Entities.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beehouse.Essentials.BeAuth.Services
{
    public class SubscriptionService
    {
        private readonly BeAuthContext _context;

        public SubscriptionService(BeAuthContext context)
        {
            _context = context;
        }

        public async Task FillSubscriptions(BeAuthIdentity user)
        {
            var set = _context.Set<Subscription>();
            var subscriptions = await set.Where(_ => _.User == user.Id)
                .Include(_ => _.Products)
                .ToListAsync();

            await ValidateSubscriptions(subscriptions, user);
        }

        public async Task ValidateSubscriptions(ICollection<Subscription> subscriptions, BeAuthIdentity user)
        {
            if (subscriptions == null || subscriptions.Count == 0)
            {
                subscriptions = new List<Subscription>
                {
                    CreateDefaultSubscription(user)
                };

            }

            user.Subscriptions = subscriptions;
        }

        private static Subscription CreateDefaultSubscription(BeAuthIdentity user)
        {
            return new Subscription
            {
                Owner = user.Id,
                User = user.Id,
                Status = SubscriptionStatus.Active,
                Products = new List<SubscriptionProduct>
                {
                    new SubscriptionProduct
                    {
                        Id = "",
                        ProductId = "",
                        Product = new Product {Id = "", Name = "Default"}
                    }
                }
            };
        }
    }
}
