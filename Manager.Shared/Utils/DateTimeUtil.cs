using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Utils
{
    public static class DateTimeUtil
    {
        // Lấy thời gian hiện tại theo múi giờ +7 (Việt Nam)
        public static DateTime GetCurrentTimeInVietnam()
        {
            //TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            //return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

            return DateTime.Now;    
        }

        // So sánh thời gian hiện tại với thời gian đầu vào
        // Trả về true nếu thời gian hiện tại nhỏ hơn hoặc bằng thời gian đầu vào, ngược lại trả về false
        public static bool CompareWithCurrentTimeInVietnam(DateTime inputTime)
        {
            DateTime currentTimeInVietnam = GetCurrentTimeInVietnam();
            return currentTimeInVietnam <= inputTime;
        }

        // Lấy thời gian còn lại so với thời gian đầu vào
        // Trả về TimeSpan là khoảng thời gian còn lại, hoặc TimeSpan.Zero nếu thời gian đầu vào đã qua
        public static TimeSpan GetTimeRemaining(DateTime inputTime)
        {
            DateTime currentTimeInVietnam = GetCurrentTimeInVietnam();
            if (currentTimeInVietnam <= inputTime)
            {
                return inputTime - currentTimeInVietnam;
            }
            else
            {
                return TimeSpan.Zero;
            }
        }
    }
}
