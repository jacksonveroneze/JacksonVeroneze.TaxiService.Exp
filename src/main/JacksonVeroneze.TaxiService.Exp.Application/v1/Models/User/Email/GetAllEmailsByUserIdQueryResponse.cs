using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;

public sealed record GetAllEmailsByUserIdQueryResponse : DataResponse<ICollection<EmailResponse>>;