using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPVU.Models
{
    public static class Configuration
    {
        public static string AuthPVUToken = "";
        public static List<string> NotificationEmails = new List<string>();
        public static bool SendEmailNotification = true;
        public static string SmtpServer = "";
        public static bool SmtpServerSSL = true;
        public static int SmtpPort = 0;
        public static string SmtpUserName = "";
        public static string SmtpPassword = "";
    }
}
