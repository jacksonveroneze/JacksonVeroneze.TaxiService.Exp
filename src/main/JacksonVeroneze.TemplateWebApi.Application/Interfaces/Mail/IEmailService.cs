using JacksonVeroneze.TemplateWebApi.Application.Models.Infra;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Mail;

public interface IEmailService
{
    Task SendAsync(EmailRequest request,
        CancellationToken cancellationToken);
}
