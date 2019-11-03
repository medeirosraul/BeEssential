using Beehouse.Essentials.BeAuth;
using Beehouse.Essentials.BeAuthMongo.Services;
using Beehouse.Essentials.Repositories;
using Beehouse.Essentials.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.BeAuthMongo
{
    public static class BeeAuthMongoExtensions
    {
        public static IServiceCollection AddBeeAuthMongo(this IServiceCollection services)
        {
            services.AddScoped(typeof(IServiceBase<,,>), typeof(IdentifiableService<>));
            services.AddBeAuth();

            return services;
        }
    }
}
