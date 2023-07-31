using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Extension
{
    public static class Extention
    {
        public static long ToTimeStamp(this DateTime value)
        {
            try
            {
                long unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                return unixTimestamp;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DateTime ToStartDate(this DateTime value)
        {
            try
            {
                return new DateTime(value.Year,
                                      value.Month,
                                      value.Day,
                                      0, 0, 0, 0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DateTime ToEndDate(this DateTime value)
        {
            try
            {
                return new DateTime(value.Year,
                                      value.Month,
                                      value.Day,
                                      23, 59, 59, 999);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}