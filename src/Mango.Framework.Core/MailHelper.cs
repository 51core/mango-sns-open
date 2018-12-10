using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;
namespace Mango.Framework.Core
{
    /// <summary>
    /// 电子邮件发送辅助类
    /// </summary>
    public class MailHelper
    {
        public static bool SendEmail(string email, string subject, string message)
        {
            bool sendResult = false;
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("51Core.Net", "service@51core.net"));
                emailMessage.To.Add(new MailboxAddress("mail", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("plain") { Text = message };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.mxhichina.com", 465, true);
                    client.Authenticate("service@51core.net", "Yu19880804");

                    client.Send(emailMessage);
                    client.Disconnect(true);
                    sendResult = true;
                }
            }
            catch
            {
                sendResult = false;
            }
            return sendResult;
        }
    }
}
