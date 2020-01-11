using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Types.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<Paged<T>> ToPagedAsync<T>(this IQueryable<T> queryable) where T: class
        {
            var paged = new Paged<T>
            {
                Total = await queryable.CountAsync(),
                Page = 1
            };

            paged.Limit = paged.Total;
          
            if (paged.Total == 0) return paged;

            paged.AddRange(await queryable.ToListAsync());

            return paged;
        }

        public static async Task<Paged<T>> ToPagedAsync<T>(this IQueryable<T> queryable, int page, int limit) where T: class
        {
            var paged = new Paged<T>
            {
                Total = await queryable.CountAsync(),
                Page = page,
                Limit = limit
            };

            if (paged.Total == 0) return paged;

            paged.AddRange(await queryable.Skip((page - 1) * limit).Take(limit).ToListAsync());

            return paged;
        }
    }
}
