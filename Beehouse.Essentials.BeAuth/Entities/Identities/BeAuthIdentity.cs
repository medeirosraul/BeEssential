using Beehouse.Essentials.BeAuth.Entities.Subscriptions;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace Beehouse.Essentials.BeAuth.Entities.Identities
{
    public class BeAuthIdentity : IdentityUser
    {
        public ICollection<Subscription> Subscriptions{ get; set; }
        public string ActiveSubscription { get; set; }
        public BeAuthProfile Profile { get; set; }
    }
}