using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer.Application.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime UnixStart => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static double ToUnixTime(this DateTime dateTime)
        {
            return (dateTime - UnixStart).TotalMilliseconds;
        }

        public static DateTime FromUnixTime(double milliseconds)
        {
            return UnixStart.AddMilliseconds(milliseconds);
        }

    }
}
