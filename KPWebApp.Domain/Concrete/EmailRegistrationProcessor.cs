using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Entities;

namespace KPWebApp.Domain.Concrete
{
    public class EmailRegistrationProcessor : IRegistrationProcessor
    {
        private EmailSettings emailSettings;

        public EmailRegistrationProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessOrder(string firstName, string middleName, string lastName, string email, string username)
        {
            this.emailSettings.MailToAddress = email;
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username,
                emailSettings.Password);
                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder();
                body.AppendLine(string.Format("Вітаємо, {0} {1}!", firstName, middleName));
                body.Append(string.Format("Ви зареєструвалися на сторінці кафедри програмування під логіном {0}",
                    username));
                body.AppendLine(
                    string.Format("Якщо ви не {0} {1} {2}, звернітся до адміністратора сайту: adminkp@gmail.com",
                        firstName, middleName, lastName));
                body.AppendLine("Дякуємо! Щасти!");
                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "Реєстрація на сторінці КП",
                    body.ToString());
                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
            }
        }
    }
}
