using System;
using System.Collections.Generic;
using Backpack.Extensions.IEnumerable;

namespace Backpack.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToCommonDateString(this DateTime dateTime)
        {
            return dateTime.ToString("D");
        }
    }
}