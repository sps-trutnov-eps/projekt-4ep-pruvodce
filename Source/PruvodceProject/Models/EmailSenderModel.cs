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
            string senderMail = "skolniEmailSpsTrutnov@outlook.com";
            //Sem zadat heslo od emailu.
            string senderPassword = "=3#2moBGu4_O06==W06p9O-OR%45kaA3(+Hil7$q(!58&*$B$5j$-&T=7L)i-0c%6C8w7-zT9Y8r*)63iHv&z&3)4pM+S6c6kh%E";

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