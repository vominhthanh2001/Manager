using BlazorBootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Web.Utils
{
    public static class ToastMessageUtil
    {
        public static List<ToastMessage> Messages = new List<ToastMessage>();
        public static void ShowMessage(ToastType toastType, string message) => Messages.Add(CreateToastMessage(toastType, message));
        private static ToastMessage CreateToastMessage(ToastType toastType, string message)
        {
            return new ToastMessage
            {
                Type = toastType,
                Title = "Hệ Thống",
                HelpText = $"{DateTime.Now}",
                Message = $"{message}.",
            };
        }
    }
}
