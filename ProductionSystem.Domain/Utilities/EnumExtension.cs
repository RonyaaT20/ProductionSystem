using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem.Domain.Utilities
{
    public static class EnumExtensions
    {
        public static int ToValue<T>(this T input) where T : struct
        {
            return Convert.ToInt32(input);
        }

        public static string ToDisplay(this Enum value)
        {
            try
            {
                var attribute = value?.GetType().GetField(value.ToString()).GetCustomAttributes(false).FirstOrDefault();

                if (attribute != null)
                    return value?.ToString();

                var propValue = attribute?.GetType().GetProperty("Name").GetValue(attribute, null);
                return propValue?.ToString();
            }
            catch (Exception e)
            {
                return "-";
            }
        }
    }
}
