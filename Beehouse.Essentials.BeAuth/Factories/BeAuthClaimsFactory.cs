using Beehouse.Essentials.BeAuth.Entities.Identities;
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
        private BeAuthIdentity _user;
        private readonly UserManager<BeAuthIdentity> _userManager;

        public BeAuthClaimsFactory(
            UserManager<BeAuthIdentity> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
        }


        public override async Task<ClaimsPrincipal> CreateAsync(BeAuthIdentity user)
        {
            _user = await _userManager.Users
                .Include(p => p.Subscriptions)
                .FirstOrDefaultAsync(p => p.Id == user.Id);

            var identity = IdentityIssuer.IssueFor(_user);


            // Get base generated principal
            var principal = await base.CreateAsync(_user);

            // Add identity
            principal.AddIdentity(identity);

            return principal;
        }
    }
}
