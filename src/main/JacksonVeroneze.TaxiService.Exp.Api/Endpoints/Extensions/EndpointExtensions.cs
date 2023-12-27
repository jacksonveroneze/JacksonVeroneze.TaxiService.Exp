namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;

public static class EndpointExtensions
{
    public static RouteHandlerBuilder AddDefaultResponseEndpoints(
        this RouteHandlerBuilder builder)
    {
        builder.ProducesValidationProblem()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);

        return builder;
    }
}