using DNTPersianUtils.Core;
using System;

namespace ProductionSystem.Application.Helpers
{
    public static class DateHelper
    {
        // میلادی به شمسی
        public static string ToShamsi(this DateTime date)
        {
            return date.ToShortPersianDateString();
        }

        public static string ToShamsi(this DateTime? date)
        {
            if (date == null) return "-";
            return date.Value.ToShortPersianDateString();
        }

        // شمسی به میلادی
        public static DateTime ToMiladi(this string shamsiDate)
        {
            try
            {
                return shamsiDate.ToGregorianDateTime() ?? DateTime.Now;
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static DateTime? ToMiladiNullable(this string shamsiDate)
        {
            if (string.IsNullOrEmpty(shamsiDate)) return null;
            try
            {
                return shamsiDate.ToGregorianDateTime();
            }
            catch
            {
                return null;
            }
        }
    }
}