using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Mail;
using JacksonVeroneze.TemplateWebApi.Application.Models.Infra;
using JacksonVeroneze.TemplateWebApi.Domain.Parameters;
using MimeKit;
using MailKit.Security;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mail;

public class SmtpMailService : IMailService
{
    private readonly MailParameters _parameters;

    public SmtpMailService(MailParameters parameters)
    {
        _parameters = parameters;
    }

    public async Task SendEmailAsync(MailRequest mailRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            MimeMessage mail = new();

            mail.From.Add(new MailboxAddress(_parameters.DisplayName,
                mailRequest.From ?? _parameters.From));

            mail.Sender = new MailboxAddress(mailRequest.DisplayName ?? _parameters.DisplayName,
                mailRequest.From ?? _parameters.From);

            mail.To.AddRange(mailRequest.To
                .Where(item => !string.IsNullOrEmpty(item))
                .Select(MailboxAddress.Parse));

            if (!string.IsNullOrEmpty(mailRequest.ReplyTo))
                mail.ReplyTo.Add(new MailboxAddress(
                    mailRequest.ReplyToName, mailRequest.ReplyTo));

            mail.Bcc.AddRange(mailRequest.Bcc
                .Where(item => !string.IsNullOrEmpty(item))
                .Select(MailboxAddress.Parse));

            mail.Cc.AddRange(mailRequest.Cc
                .Where(item => !string.IsNullOrEmpty(item))
                .Select(MailboxAddress.Parse));

            BodyBuilder body = new();
            mail.Subject = mailRequest?.Subject;
            body.HtmlBody = mailRequest?.Body;
            mail.Body = body.ToMessageBody();

            using MailKit.Net.Smtp.SmtpClient smtp = new();

            if (_parameters.UseSsl)
            {
                await smtp.ConnectAsync(_parameters.Host,
                    _parameters.Port,
                    SecureSocketOptions.SslOnConnect,
                    cancellationToken);
            }
            else if (_parameters.UseStartTls)
            {
                await smtp.ConnectAsync(_parameters.Host,
                    _parameters.Port,
                    SecureSocketOptions.StartTls,
                    cancellationToken);
            }

            await smtp.AuthenticateAsync(_parameters.UserName,
                _parameters.Password, cancellationToken);

            await smtp.SendAsync(mail, cancellationToken);

            await smtp.DisconnectAsync(true, cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
