using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.Types
{
    public class Paged<T> : List<T>
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
    }
}
