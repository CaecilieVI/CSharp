using System;
using System.Globalization;

namespace BDSA2017.Exercise01
{
    public class Calculator
    {
        public static bool IsLeapYear(int year)
        {
            if (year < 1582)
            {
                throw new ArgumentException("Year must be greater than 1582", nameof(year));
            }
            if (year > 9999)
            {
                throw new ArgumentException("Year must less than 10000", nameof(year));
            }

            return year % 4 == 0 && ((year % 100 != 0) || (year % 100 == 0 && year % 400 == 0));
        }

        public static bool IsFrameworkLeapYear(int year)
        {
            var calendar = new GregorianCalendar();
            return calendar.IsLeapYear(year);
        }

        public static bool IsPowerOf(int number, int power)
        {
            if(power == 0)
            {
                return number == 1 || number == 0;
            }

            if(number % power == 0)
            {
                return IsPowerOf(number / power, power);
            }
            else
            {
                return number == 1;
            }
        }
    }
}
