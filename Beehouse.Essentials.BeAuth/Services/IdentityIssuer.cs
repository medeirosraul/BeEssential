using Beehouse.Essentials.BeAuth.Entities.Identities;
using Beehouse.Essentials.BeAuth.Entities.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Beehouse.Essentials.BeAuth.Services
{
    public static class IdentityIssuer
    {
        public static ClaimsIdentity IssueFor(BeAuthIdentity user)
        {
            //Cache operations

            // Get subscription information.
            var subscription = user.Subscriptions.FirstOrDefault(p => p.Number == user.ActiveSubscription);
            subscription ??= user.Subscriptions.FirstOrDefault();

            // Issue
            var identity = new ClaimsIdentity("BeAuthIdentity");
            identity.AddClaim(new Claim(SubscriptionClaimTypes.User, subscription.User));
            identity.AddClaim(new Claim(SubscriptionClaimTypes.Owner, subscription.Owner));

            foreach(var product in subscription.Products)
            {
                identity.AddClaim(new Claim(SubscriptionClaimTypes.Product, product.Product.Name));
            }

            return identity;
        }
    }
}
