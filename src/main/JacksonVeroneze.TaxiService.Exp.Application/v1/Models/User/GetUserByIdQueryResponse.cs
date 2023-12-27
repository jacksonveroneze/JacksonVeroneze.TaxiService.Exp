using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;

public sealed record GetUserByIdQueryResponse : DataResponse<UserResponse>;