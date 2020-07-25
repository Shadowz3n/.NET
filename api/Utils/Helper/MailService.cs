using System.Collections.Generic;
using System.Web.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace API.Utils.Helper
{
    /// <summary>
    /// Mail service.
    /// </summary>
    public class MailService
    {
        /// <summary>
        /// Sends the mail async.
        /// </summary>
        /// <returns><c>true</c>, if mail async was sent, <c>false</c> otherwise.</returns>
        /// <param name="sendTo">Send to.</param>
        /// <param name="replyTo">Reply to.</param>
        /// <param name="title">Title.</param>
        /// <param name="html">Html.</param>
        public static bool SendMailAsync(string sendTo, string[] replyTo, string title, string html)
        {
            List<object> returnResponse = new List<object>();
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(WebConfigurationManager.AppSettings["MailFrom"].Trim()));
            message.To.Add(new MailboxAddress(sendTo.Trim()));
            message.Subject = title;
            message.Body = new TextPart("html") { Text = html };

            // Bcc to
            if (replyTo.Length > 0)
            {
                foreach (string reply in replyTo)
                {
                    message.ResentBcc.Add(new MailboxAddress(reply.Trim()));
                }
            }

            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    int mailPort = int.TryParse(WebConfigurationManager.AppSettings["MailPort"], out mailPort) ? mailPort : 0;
                    SecureSocketOptions emailSecurity = WebConfigurationManager.AppSettings["MailSsl"] == "true" ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls;
                    client.Connect(WebConfigurationManager.AppSettings["MailServer"], mailPort, false);
                    client.Authenticate(WebConfigurationManager.AppSettings["MailUser"], WebConfigurationManager.AppSettings["MailPass"]);
                    client.Send(message);
                    client.Disconnect(true);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
