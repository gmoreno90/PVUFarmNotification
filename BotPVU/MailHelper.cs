using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace BotPVU
{
    public static class MailHelper
    {
        public static void sendEmail(string Subject, string Body)
        {
            try
            {
                if (Models.Configuration.SendEmailNotification)
                {
                    MimeMessage message = new MimeMessage();

                    MailboxAddress from = new MailboxAddress(Models.Configuration.SmtpUserName, Models.Configuration.SmtpUserName);
                    message.From.Add(from);
                    foreach (var item in Models.Configuration.NotificationEmails)
                    {
                        message.To.Add(new MailboxAddress("", item));
                    }

                    message.Priority = MessagePriority.Urgent;
                    message.Importance = MessageImportance.High;
                    message.Subject = Subject;
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = Body;
                    bodyBuilder.TextBody = Body;

                    message.Body = bodyBuilder.ToMessageBody();

                    SmtpClient client = new SmtpClient();
                    if (Models.Configuration.SmtpServerSSL)
                        client.Connect(Models.Configuration.SmtpServer, Models.Configuration.SmtpPort, SecureSocketOptions.StartTls);
                    else
                        client.Connect(Models.Configuration.SmtpServer, Models.Configuration.SmtpPort);

                    client.Authenticate(Models.Configuration.SmtpUserName, Models.Configuration.SmtpPassword);
                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR EMAIL " + ex.Message + "\n" + ex.StackTrace);
            }

        }
    }
}
