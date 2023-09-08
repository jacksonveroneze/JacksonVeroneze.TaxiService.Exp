using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Models.User.Email;

public sealed record GetAllEmailsByUserIdQueryResponse : DataResponse<ICollection<EmailResponse>>;
