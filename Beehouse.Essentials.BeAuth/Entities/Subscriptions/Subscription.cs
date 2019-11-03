using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.BeAuth.Entities.Subscriptions
{
    public class Subscription
    {
        public string Number { get; set; }
        public string User { get; set; }
        public string Owner { get; set; }
        public SubscriptionStatus Status { get; set; }
        public ICollection<SubscriptionProduct> Products { get; set; }
    }
}