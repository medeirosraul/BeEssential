using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.Types
{
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
