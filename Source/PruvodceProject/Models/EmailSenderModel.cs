using System.Net;
using System.Net.Mail;
using PruvodceProject.Interfaces;

namespace PruvodceProject.Models
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            string senderMail = "spspruvodce@gmail.com";
            string senderPassword = "Password";

            var client = new SmtpClient("smtp-mail@gmail.com", 587)
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