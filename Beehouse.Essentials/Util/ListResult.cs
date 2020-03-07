using Beehouse.Essentials.Entities;
using Beehouse.Essentials.Repositories;
using Beehouse.Essentials.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beehouse.Essentials.Util
{
    public class ListResult<T>
    {
        private BaseService<BaseEntity, IBaseRepository<IQueryable<BaseEntity>, BaseEntity>, IQueryable<BaseEntity>> _service;
        private Func<object, T> _translator;
        private Func<ListResult<T>> _fill;


        public ICollection<T> List { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Count { get; set; }
    }
}
