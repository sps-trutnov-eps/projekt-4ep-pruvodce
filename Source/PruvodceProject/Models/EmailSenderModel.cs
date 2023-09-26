using System.Net;
using System.Net.Mail;
using PruvodceProject.Interfaces;

namespace PruvodceProject.Models
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            //Sem zadat email od koho se to bude odesílat.
            string senderMail = Environment.GetEnvironmentVariable("EMAIL_UCET").ToString();
            //Sem zadat heslo od emailu.
            string senderPassword = Environment.GetEnvironmentVariable("HESLO_EMAIL").ToString();

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(senderMail, senderPassword)
            };

            return client.SendMailAsync(
                new MailMessage(from: senderMail,
                                to: email,
                                subject,
                                message
                                ));

        }
    }
}