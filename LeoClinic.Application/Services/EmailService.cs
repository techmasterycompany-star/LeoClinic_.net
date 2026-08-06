using LeoClinic.Application.DTOs;
using LeoClinic.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;

namespace LeoClinic.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettingsOptions)
        {
            _emailSettings = emailSettingsOptions.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string bodyHtml)
        {
            if (string.IsNullOrWhiteSpace(_emailSettings.SmtpServer))
            {
                return;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html) { Text = bodyHtml };

            using var smtp = new SmtpClient();
            var socketOptions = _emailSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, socketOptions);

            if (!string.IsNullOrEmpty(_emailSettings.Username))
            {
                await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            }

            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}
