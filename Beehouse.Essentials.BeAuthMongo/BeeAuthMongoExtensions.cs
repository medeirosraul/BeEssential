using Beehouse.Essentials.BeAuth;
using Beehouse.Essentials.BeAuthMongo.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Beehouse.Essentials.BeAuthMongo
{
    public static class BeeAuthMongoExtensions
    {
        public static IServiceCollection AddBeeAuthMongo(this IServiceCollection services)
        {

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(Services.IService<>), typeof(Services.Service<>));
            services.AddBeAuth();

            return services;
        }
    }
}
