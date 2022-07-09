using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public static class StringExtensions
    {
        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !
                string.IsNullOrEmpty(value);
        }

        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
   
        }

        public static decimal ToDecimal(this string value)
        {
            return Convert.ToDecimal(value);

        }


        public static string ToNumeric(this int value)
        {
            return value.ToString("NO");
        }

        public static string ToNumeric(this decimal value)
        {
            return value.ToString("NO");
        }


        public static string Fa2En(this string str)
        {
            return str.Replace("۰", "0")
                   .Replace("۱", "1")
                   .Replace("۲", "2")
                   .Replace("۳", "3")
                   .Replace("۴", "4")
                   .Replace("۵", "5")
                   .Replace("۶", "6")
                   .Replace("۷", "7")
                   .Replace("۸", "8")
                   .Replace("۹", "9");
        }

        public static string FixPersianChars(this string str)
        {
            return str.Replace("ک", "ک")
                   .Replace("ک", "ک")
                   .Replace("ک", "ک")
                   .Replace("ک", "ک")
                   .Replace("ک", "ک")
                   .Replace("ي", "ی")
                   .Replace("", " ")
                   .Replace("", "ه");
        }


        public static string CleanString(this string str)
        {
            return str.Trim().FixPersianChars().Fa2En().NullIfEmpty();
        }


        public static string NullIfEmpty(this string str)
        {
            return str?.Length == 0 ? null : str;
        }
         

    }
}
