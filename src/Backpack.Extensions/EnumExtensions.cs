using System;
using System.ComponentModel;
using System.Reflection;

namespace Backpack.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the Description value defined the attribute
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescriptionString(this Enum value)
        {
            Type type = value.GetType();

            MemberInfo[] memInfo = type.GetMember(value.ToString());

            if (memInfo.Length > 0)
            {
                object[] description = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (description.Length > 0)
                {
                    return ((DescriptionAttribute)description[0]).Description;
                }
            }

            return value.ToString();
        } 
    }
}