using ASP_RAZOR_5.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ASP_RAZOR_5.Services
{
    public class SendMailService : IEmailSender
    {

        private readonly MailSettings mailSettings;
        private readonly ILogger<SendMailService> logger;

        public SendMailService(IOptions<MailSettings> mailSettings, ILogger<SendMailService> logger)
        {
            this.mailSettings = mailSettings.Value;
            this.logger = logger;
            logger.LogInformation("Created sendmailservice");
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            message.To.Add( MailboxAddress.Parse(email));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody= htmlMessage;
            message.Body = builder.ToMessageBody();
            
            using var smtp  = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(message);

                smtp.Disconnect(true);
                logger.LogInformation("Gui email thanh cong");
            } catch ( Exception ex )
            {
                Directory.CreateDirectory("mailssave");
                var emailSaveFile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await message.WriteToAsync(emailSaveFile);

                logger.LogInformation("Loi gui email, luu tai - " + emailSaveFile);
                logger.LogInformation(ex.ToString());
            }
        }
    }
}
