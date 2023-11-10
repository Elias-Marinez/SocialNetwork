
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using SocialNetwork.Core.Application.Dtos.Email;
using SocialNetwork.Core.Application.Interfaces;
using SocialNetwork.Core.Domain.Settings;

namespace SocialNetwork.Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        public MailSettings MailSettings { get; }
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            MailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(EmailRequest request)
        {
            try 
            {
                var email = new MimeMessage();

                email.Sender = MailboxAddress.Parse(request.From ?? MailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();

                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                smtp.Connect(MailSettings.SmtpHost, MailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(MailSettings.SmtpUser, MailSettings.SmtpPass);

                await smtp.SendAsync(email);

                smtp.Disconnect(true);
            }
            catch(Exception ex) 
            {
                _logger.LogError("Mail Sending Error" + ex.Message, ex.ToString());
            }
        }   
    }
}
