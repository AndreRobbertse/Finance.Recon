using System;

namespace Recon
{
    public static class FinanceExtensions
    {
        public static decimal ToDecimal(this string str)
        {
            return Decimal.Parse(str);
        }
    }
}