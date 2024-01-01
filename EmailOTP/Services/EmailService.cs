using EmailOTP.Models;
using EmailOTP.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EmailOTP.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSetting)
        {
            _emailSettings = emailSetting.Value;
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SMTPUsername),
                Subject = subject,
                Body = body
            };
            mailMessage.To.Add(to);

            using var smtpClient = new SmtpClient(_emailSettings.SMPTHost, _emailSettings.SMTPPort)
            {
              Credentials = new NetworkCredential(_emailSettings.SMTPUsername, _emailSettings.SMTPPassword),
              EnableSsl = true
            };
                
            await smtpClient.SendMailAsync(mailMessage);
                

        }
        
    }
}
