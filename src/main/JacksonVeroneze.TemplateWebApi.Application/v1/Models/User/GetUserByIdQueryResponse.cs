using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;

public sealed record GetUserByIdQueryResponse : DataResponse<UserResponse>;