namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Directives;

public class CpfDirectiveType : DirectiveType
{
    private const string DirectiveName = "cpf";

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
                context.Result = context.Result = Convert.ToUInt64(result)
                    .ToString(@"000\.000\.000\-00");
            }
        });
    }
}