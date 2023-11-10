using SocialNetwork.Core.Application.Dtos.Email;
using SocialNetwork.Core.Domain.Settings;

namespace SocialNetwork.Core.Application.Interfaces
{
    public interface IEmailService
    {
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}
