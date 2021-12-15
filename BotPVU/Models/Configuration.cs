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
        public static bool AutoFarming = true;
        public static int AutoFarmingDelay = 0;
        public static string UserAgent = "";
        public static string BackendEndpoint = "";
        public static List<string> MyPlantsSpring = new List<string>();
        public static List<string> MyPlantsSummer = new List<string>();
        public static List<string> MyPlantsAutumn = new List<string>();
        public static List<string> MyPlantsWinter = new List<string>();
        public static bool PrintLogResponses = true;
        public static List<string> ResponseMessages = new List<string>();
    }
}
