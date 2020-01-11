using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Beehouse.Essentials.Mongo
{
    public static class IMongoQueryableExtensions
    {
        public static IMongoQueryable<T> TextSearch<T>(this IMongoQueryable<T> query, string search)
        {
            var filter = Builders<T>.Filter.Text(search);
            return query.Where(_ => filter.Inject());
        }
    }
}
