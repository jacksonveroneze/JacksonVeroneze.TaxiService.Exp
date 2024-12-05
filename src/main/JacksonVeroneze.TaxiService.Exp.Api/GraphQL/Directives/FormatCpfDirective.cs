namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Directives;

internal class FormatCpfDirective : DirectiveType
{
    private const string DirectiveName = "formatcpf";

    private const string Mask = @"000\.000\.000\-00";

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
                context.Result = context.Result =
                    Convert.ToUInt64(result).ToString(Mask);
            }
        });
    }
}