using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.Util
{
    public class ListResult<T>
    {
        public ICollection<T> List { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Count { get; set; }
    }
}
