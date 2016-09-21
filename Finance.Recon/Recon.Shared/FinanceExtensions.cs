using System;
using System.Collections.Generic;
using System.Linq;

namespace Recon
{
    public static class FinanceExtensions
    {
        public static decimal ToDecimal(this string str)
        {
            return Decimal.Parse(str);
        }
        public static IEnumerable<T> ToIEnumerable<T>(this T source) where T : new()
        {
            return new[] { source };
        }
    }
}