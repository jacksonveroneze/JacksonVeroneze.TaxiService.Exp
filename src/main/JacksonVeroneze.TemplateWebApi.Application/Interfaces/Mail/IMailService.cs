using JacksonVeroneze.TemplateWebApi.Application.Models.Infra;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Mail;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest,
        CancellationToken cancellationToken);
}
