namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public enum ResponseStatus
{
    None,
    Ok = 200,
    Created = 201,
    NoContent = 204,
    BadRequest = 400,
    NotFound = 404,
}
