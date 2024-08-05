using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Models
{
    public class VoucherModel
    {
        public List<TimeSpan> Vouchers { get; set; } = new List<TimeSpan>
        {
            TimeSpan.FromDays(3),
            TimeSpan.FromDays(5),
            TimeSpan.FromDays(7),
            TimeSpan.FromDays(15),
            TimeSpan.FromDays(30),
        };

        public static string ConvertTextToTime(TimeSpan span)
        {
            string text = $"{span.Days} ngày";
            return text;
        }
    }
}

