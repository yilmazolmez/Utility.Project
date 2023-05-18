using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Project.Core.Extensions
{
    public static class StringExtensions
    {

        public static bool IsInt(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            int tmp;
            return int.TryParse(value, out tmp);
        }

        public static bool IsInt64(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            long tmp;
            return long.TryParse(value, out tmp);
        }

        public static bool IsDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            DateTime tmp;
            if (DateTime.TryParse(value, out tmp))
            {
                if (Convert.ToDateTime(value) == DateTime.MinValue || Convert.ToDateTime(value) == DateTime.MaxValue)
                    return false;

                return true;
            }

            return false;
        }

        public static bool IsNumeric(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            decimal convert;

            return decimal.TryParse(value, out convert);
        }

        public static bool IsBoolean(this string value)
        {
            var val = value.ToLower().Trim();

            if (val == "1" || val == "0") return true;

            if (val == "true" || val == "false")
                return true;

            return false;
        }


        public static bool IsNull(this object objectToCall)
        {
            return objectToCall == null || Convert.IsDBNull(objectToCall);
        }

        public static bool IsNotNull(this object objectToCall)
        {
            return !IsNull(objectToCall);
        }


        public static bool IsNullOrEmpty(this string text)
        {
            return text == null || text.Trim().Length == 0 || string.IsNullOrEmpty(text);
        }

        public static bool IsNullOrEmpty(this char value)
        {
            return value.ToString().IsNullOrEmpty();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return value.IsNull() || !value.Any();
        }


        public static bool IsNotNullOrEmpty(this string text)
        {
            return !text.IsNullOrEmpty();
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return !value.IsNullOrEmpty();
        }

        //public static bool IsAnyOf(this string currentValue, bool isCaseSensitive = false, params string[] values)
        //{
        //    return values?.Any((string p) => (p ?? string.Empty).IsEqual(currentValue ?? string.Empty, isCaseSensitive)) ?? currentValue.IsNullOrEmpty();
        //}

        //public static bool IsAnyOf(this string currentValue, params string[] values)
        //{
        //    return currentValue.IsAnyOf(isCaseSensitive: false, values);
        //}

        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase: true);
        }

        public static TEnum ToEnum<TEnum>(this string value, TEnum defaultValue) where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            if (!Enum.TryParse<TEnum>(value, ignoreCase: true, out var result))
            {
                return defaultValue;
            }

            return result;
        }
    }
}
