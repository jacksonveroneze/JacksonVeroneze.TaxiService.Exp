using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.User;

public sealed record GetUserByIdQueryResponse : DataResponse<UserResponse>;
