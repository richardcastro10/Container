using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Container.Shared.Enums
{
    public static class EnumExtensions
    {

        public static string GetStringValue(this Enum e)
        {
            var attribute =
            e.GetType()
                .GetTypeInfo()
                .GetMember(e.ToString())
                .FirstOrDefault(member => member.MemberType == MemberTypes.Field)
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault()
                as DescriptionAttribute;

            return attribute?.Description ?? e.ToString();
        }

        public static string GetStringValue<TEnum>(this short value)
        {
            return ((int)value).GetStringValue<TEnum>();
        }

        public static string GetStringValue<TEnum>(this int value)
        {
            return ((Enum)Enum.ToObject(typeof(TEnum), value)).GetStringValue();
        }
        public static TEnum ToEnum<TEnum>(this int value) where TEnum : struct, Enum
        {
            TEnum defaultValue;
            return Enum.TryParse(value.ToString(), out defaultValue) ? defaultValue : (TEnum)Enum.GetValues(typeof(TEnum)).GetValue(0);
        }
    }
}
