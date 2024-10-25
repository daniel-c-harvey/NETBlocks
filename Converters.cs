using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Converters
{
    public class IntConverter
    {
        public static string ToString(int value)
        {
            return value.ToString();
        }

        public static int FromString(string value) 
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }

            throw new ArgumentException("Failed to convert string to integer");
        }
    }

    public class LongConverter
    {
        public static string ToString(long value)
        {
            return value.ToString();
        }

        public static long FromString(string value)
        {
            long result;
            if (long.TryParse(value, out result))
            {
                return result;
            }

            throw new ArgumentException("Failed to convert string to long");
        }
    }

    public class DateTimeConverter
    {
        public static string ToShortDate(DateTime value)
        {
            return value.ToShortDateString();
        }

        public static DateTime FromString(string value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }

            throw new ArgumentException("Failed to convert string to DateTime");
        }
    }
}
