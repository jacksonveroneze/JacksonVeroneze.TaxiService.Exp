namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public abstract record NoContentResponse() :
    BaseResponse(ResponseStatus.NoContent);
