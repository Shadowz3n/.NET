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
        private readonly static string _mailFrom = WebConfigurationManager.AppSettings["MailFrom"].Trim();
        private readonly static string _mailServer = WebConfigurationManager.AppSettings["MailServer"].Trim();
        private readonly static string _mailUser = WebConfigurationManager.AppSettings["MailUser"].Trim();
        private readonly static string _mailPass = WebConfigurationManager.AppSettings["MailPass"].Trim();
        private readonly static string _mailPort = WebConfigurationManager.AppSettings["MailPort"].Trim();
        private readonly static SecureSocketOptions _mailSsl = WebConfigurationManager.AppSettings["MailSsl"] == "true" ? 
                                                                SecureSocketOptions.SslOnConnect 
                                                                : SecureSocketOptions.StartTls;

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
            message.From.Add(new MailboxAddress(_mailFrom, _mailFrom));
            message.To.Add(new MailboxAddress(sendTo.Trim(), sendTo.Trim()));
            message.Subject = title;
            message.Body = new TextPart("html") { Text = html };

            // Bcc to
            if (replyTo.Length > 0)
            {
                foreach (string reply in replyTo)
                {
                    message.ResentBcc.Add(new MailboxAddress(reply.Trim(), reply.Trim()));
                }
            }

            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    int mailPort = int.TryParse(_mailPort, out mailPort) ? mailPort : 0;
                    client.Connect(_mailServer, mailPort, false);
                    client.Authenticate(_mailUser, _mailPass);
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
