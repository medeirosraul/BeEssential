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
        public static async Task<PagedList<T>> ToPagedAsync<T>(this IQueryable<T> queryable) where T: class
        {
            var paged = new PagedList<T>
            {
                TotalCount = await queryable.CountAsync(),
                PageIndex = 1
            };

            paged.PageSize = paged.TotalCount;
          
            if (paged.TotalCount == 0) return paged;

            paged.AddRange(await queryable.ToListAsync());

            return paged;
        }

        public static async Task<PagedList<T>> ToPagedAsync<T>(this IQueryable<T> queryable, int page, int limit) where T: class
        {
            var paged = new PagedList<T>
            {
                TotalCount = await queryable.CountAsync(),
                PageIndex = page,
                PageSize = limit
            };

            if (paged.TotalCount == 0) return paged;

            paged.AddRange(await queryable.Skip((page - 1) * limit).Take(limit).ToListAsync());

            return paged;
        }
    }
}
