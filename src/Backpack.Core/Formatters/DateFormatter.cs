// ************************************************** 
// © Copyright 2015 WordFly. All Rights Reserved.                  
//  Project: Backpack.Core
//  **************************************************/    

using System;

namespace Backpack.Core.Formatters
{
    /// <summary>
    /// Collection of date/time helper functions
    /// </summary>
    public static class DateFormatter
    {
        /// <summary>
        /// Function to change the incoming date to just before midnight (Sql compliant in that milliseconds is set to 997)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime DateTimeToJustBeforeMidnight(DateTime date)
        {
            return DateTime.Parse(date.ToShortDateString()).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(997);
        }

        /// <summary>
        /// Remove time from the date
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>DateTime</returns>
        public static DateTime StripTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        /// <summary>
        /// Remove milliseconds from the date
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>DateTime</returns>
        public static DateTime StripMilliseconds(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }


        /// <summary>
        /// Converts date/time from specified time zone to UTC date/time.
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <param name="timeZone">Time zone</param>
        /// <returns>Date in UTC</returns>
        public static DateTime ToSiteUtcDateTime(this DateTime date, TimeZoneInfo timeZone)
        {
            return TimeZoneInfo.ConvertTimeToUtc(date, timeZone);
        }

        /// <summary>
        /// Converts date/time from specified time zone to UTC date/time.
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <param name="timeZone">Time zone</param>
        /// <returns>Date in UTC</returns>
        public static DateTime ToUtcDateTime(this DateTime date, string timeZone)
        {
            try
            {
                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                return ToSiteUtcDateTime(date, zone);
            }
            catch { }

            return date;
        }


        /// <summary>
        /// Gets age by birthday as of today's date
        /// </summary>
        /// <param name="birthday">DateTime</param>
        /// <returns>Age as integer</returns>
        public static int AgeAsOfToday(DateTime birthday)
        {
            DateTime now = DateTime.Now;
            int result = 0;

            // Check if birthday is valid (meaning it's in the past)
            if (birthday < now)
            {
                result = now.Year - birthday.Year;

                // Special handling for people who were born on Feb 29th of the leap year
                if (birthday.Day == 29 && birthday.Month == 2 && !DateTime.IsLeapYear(now.Year))
                {
                    birthday = birthday.AddDays(-1);
                }

                DateTime birthdayThisYear = new DateTime(now.Year, birthday.Month, birthday.Day);

                // Check if person didn't have birthday this year yet and adjust age
                if (now < birthdayThisYear)
                {
                    result--;
                }
            }

            return result;

        }

        /// <summary>
        /// Returns the suffix for the specific day.  Such as "st", "nd", "th", in "1st", "2nd", "10th"
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string GetDaySuffix(int day)
        {
            string returnValue = string.Empty;

            if (day > 0 & day <= 31)
            {
                switch (day)
                {
                    case 1:
                    case 21:
                    case 31:
                        {
                            returnValue = "st";
                            break;
                        }
                    case 2:
                    case 22:
                        {
                            returnValue = "nd";
                            break;
                        }
                    case 3:
                    case 23:
                        {
                            returnValue = "rd";
                            break;
                        }
                    default:
                        {
                            returnValue = "th";
                            break;
                        }
                }
            }

            return returnValue;
        }
    }
}