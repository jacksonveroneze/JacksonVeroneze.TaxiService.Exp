using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Mail;
using JacksonVeroneze.TemplateWebApi.Application.Models.Infra;
using JacksonVeroneze.TemplateWebApi.Domain.Parameters;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mail;

public class SmtpEmailService : IEmailService
{
    private readonly MailParameters _parameters;

    public SmtpEmailService(MailParameters parameters)
    {
        _parameters = parameters;
    }

    public async Task SendAsync(EmailRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            MimeMessage mail = new();

            mail.From.Add(new MailboxAddress(_parameters.DisplayName,
                request.From ?? _parameters.From));

            mail.Sender = new MailboxAddress(request.DisplayName ?? _parameters.DisplayName,
                request.From ?? _parameters.From);

            mail.To.AddRange(request.To
                .Where(item => !string.IsNullOrEmpty(item))
                .Select(MailboxAddress.Parse));

            if (!string.IsNullOrEmpty(request.ReplyTo))
                mail.ReplyTo.Add(new MailboxAddress(
                    request.ReplyToName, request.ReplyTo));

            mail.Bcc.AddRange(request.Bcc
                .Where(item => !string.IsNullOrEmpty(item))
                .Select(MailboxAddress.Parse));

            mail.Cc.AddRange(request.Cc
                .Where(item => !string.IsNullOrEmpty(item))
                .Select(MailboxAddress.Parse));

            BodyBuilder body = new();
            mail.Subject = request?.Subject;
            body.HtmlBody = request?.Body;
            mail.Body = body.ToMessageBody();

            using SmtpClient smtp = new();

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
