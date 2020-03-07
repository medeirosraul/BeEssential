using Beehouse.Essentials.BeAuth;
using Beehouse.Essentials.BeAuthMongo.Repositories;
using Beehouse.Essentials.BeAuthMongo.Services;
using Beehouse.Essentials.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Beehouse.Essentials.BeAuthMongo
{
    public static class BeeAuthMongoExtensions
    {
        public static IServiceCollection AddBeeAuthMongo(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBaseService<,,>), typeof(Service<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddBeAuth();

            return services;
        }
    }
}
