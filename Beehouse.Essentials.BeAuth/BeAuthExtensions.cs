using Beehouse.Essentials.BeAuth.Data;
using Beehouse.Essentials.BeAuth.Entities.Identities;
using Beehouse.Essentials.BeAuth.Factories;
using Beehouse.Essentials.BeAuth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.BeAuth
{
    public static class BeAuthExtensions
    {
        public static IServiceCollection AddBeAuth(this IServiceCollection services)
        {
            services.AddScoped<SubscriptionService, SubscriptionService>();
            services.AddScoped<IUserClaimsPrincipalFactory<BeAuthIdentity>, BeAuthClaimsFactory>();
            
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            return services;
        }
    }
}
