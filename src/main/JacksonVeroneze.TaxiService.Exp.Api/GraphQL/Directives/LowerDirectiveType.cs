namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Directives;

internal class LowerDirectiveType : DirectiveType
{
    private const string DirectiveName = "lower";

    protected override void Configure(
        IDirectiveTypeDescriptor descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(DirectiveName);

        descriptor.Location(
            DirectiveLocation.FieldDefinition |
            DirectiveLocation.Field);

        descriptor.Use((next, _) => async context =>
        {
            await next(context);

            if (context.Result is string result)
            {
                context.Result = result.ToLowerInvariant();
            }
        });
    }
}