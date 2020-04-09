using Beehouse.Essentials.BeAuth.Entities.Identities;
using Beehouse.Essentials.BeAuth.Entities.Subscriptions;
using Beehouse.Essentials.BeAuth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Beehouse.Essentials.BeAuth.Factories
{
    public class BeAuthClaimsFactory : UserClaimsPrincipalFactory<BeAuthIdentity>
    {
        private readonly UserManager<BeAuthIdentity> _userManager;
        private readonly SubscriptionService _subscriptionService;

        public BeAuthClaimsFactory(
            UserManager<BeAuthIdentity> userManager,
            IOptions<IdentityOptions> optionsAccessor,
            SubscriptionService subscriptionService) : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
            _subscriptionService = subscriptionService;
        }


        public override async Task<ClaimsPrincipal> CreateAsync(BeAuthIdentity user)
        {
            // Classbase operations.
            var principal = await base.CreateAsync(user);

            // Fill subscriptions.
            await _subscriptionService.FillSubscriptions(user);

            // Issue Identity.
            var beAuthIdentity = IdentityIssuer.IssueFor(user);

            // Add identity
            //principal.AddIdentity(beAuthIdentity);
            var identity = (ClaimsIdentity) principal.Identity;
            identity.AddClaims(beAuthIdentity.Claims);

            return principal;
        }
    }
}
