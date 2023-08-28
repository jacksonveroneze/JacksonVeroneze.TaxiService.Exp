using JacksonVeroneze.TemplateWebApi.Application.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.User.Email;

public sealed record GetAllEmailsByUserIdQueryResponse : DataResponse<ICollection<EmailResponse>>;
