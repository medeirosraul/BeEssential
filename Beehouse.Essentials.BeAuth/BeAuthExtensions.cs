using Beehouse.Essentials.BeAuth.Data;
using Beehouse.Essentials.BeAuth.Entities.Identities;
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
            services.AddIdentityCore<BeAuthIdentity>()
            .AddEntityFrameworkStores<BeAuthContext>();

            return services;
        }
    }
}
