using Beehouse.Essentials.BeAuth.Entities.Identities;
using Beehouse.Essentials.BeAuth.Entities.Products;
using Beehouse.Essentials.BeAuth.Entities.Subscriptions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.BeAuth.Data
{
    public class BeAuthContext: IdentityDbContext<BeAuthIdentity>
    {
        public BeAuthContext(DbContextOptions<BeAuthContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Subscription>(b =>
            {
                b.ToTable("subscriptions");
                b.HasKey(p => p.Number);
                b.HasOne<BeAuthIdentity>().WithMany(p => p.Subscriptions).HasForeignKey(p => p.User);
                b.HasOne<BeAuthIdentity>().WithMany(p => p.Subscriptions).HasForeignKey(p => p.Owner);
                b.HasMany<SubscriptionProduct>().WithOne().HasForeignKey(p => p.SubscriptionNumber);
            });

            builder.Entity<Product>(b =>
            {
                b.ToTable("products");
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).IsRequired();
            });

            builder.Entity<SubscriptionProduct>(b =>
            {
                b.ToTable("subscription_product");
                b.HasKey(p => p.Id);
                b.HasOne<Product>().WithMany().HasForeignKey(_ => _.ProductId);
                b.HasOne<Subscription>().WithMany().HasForeignKey(_ => _.SubscriptionNumber);
            });
        }
    }
}
