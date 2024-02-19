using System.Net;
using System.Net.Mail;

namespace Aws_Login.Core.ServicesCore
{
    public class EmailService
    {
        public bool Send(string toName,
                         string toEmail,
                         string subject,
                         string body,
                         string fromName = "Teste Api Prologic",
                         string fromEmail = "sender@prologic.cloud")
        {
            var smtpClient = new SmtpClient(ConfigurationJwt.Smtp.Host, ConfigurationJwt.Smtp.Port);

            smtpClient.Credentials = new NetworkCredential(ConfigurationJwt.Smtp.UserName, ConfigurationJwt.Smtp.PassWord);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            var mail = new MailMessage();

            mail.From = new MailAddress("sender@prologic.cloud", fromName);
            mail.To.Add(new MailAddress("rubensfiorelli@outlook.com", toName));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
