using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingLite
{
    public static class BookingLiteBase
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string NullEmpty(this string str)
        {
            return str.IsNullOrEmpty() ? null : str;
        }
    }
}