using JacksonVeroneze.TemplateWebApi.Application.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.User;

public sealed record CreateUserCommandResponse : DataResponse<UserResponse>;
