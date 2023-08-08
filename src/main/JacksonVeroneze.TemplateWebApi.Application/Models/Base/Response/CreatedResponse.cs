namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public abstract record CreatedResponse() :
    BaseResponse(ResponseStatus.Created);
