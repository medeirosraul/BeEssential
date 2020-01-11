using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Beehouse.Essentials.Types
{
    public static class StringExtensions
    {
        public static decimal ToDecimal (this string s)
        {
            return Convert.ToDecimal(s);
        }
    }
}
