using Beehouse.Essentials.BeAuth.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.BeAuth.Entities.Subscriptions
{
    public class SubscriptionProduct
    {
        public string Id { get; set; }
        public string SubscriptionNumber { get; set; }
        public string ProductId { get; set; }

        public virtual Product Product {get;set;}
    }
}
