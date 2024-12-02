namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Directives;

public class UpperDirectiveType : DirectiveType
{
    private const string DirectiveName = "upper";

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
                context.Result = result.ToUpperInvariant();
            }
        });
    }
}