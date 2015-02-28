using System;

namespace Minecloud.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToCommonDateString(this DateTime dateTime)
        {
            return dateTime.ToString("D");
        }
    }
}