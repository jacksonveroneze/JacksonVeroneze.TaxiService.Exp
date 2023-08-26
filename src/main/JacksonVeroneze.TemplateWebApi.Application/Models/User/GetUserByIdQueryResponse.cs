using JacksonVeroneze.TemplateWebApi.Application.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.User;

public sealed record GetUserByIdQueryResponse : DataResponse<UserResponse>;
