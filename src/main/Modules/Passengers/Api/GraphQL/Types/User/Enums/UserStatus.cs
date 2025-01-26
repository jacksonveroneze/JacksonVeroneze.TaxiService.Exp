namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Enums;

internal class UserStatus
    : EnumType<Domain.Enums.UserStatus>
{
    private const string ItemName = "UserStatus";

    protected override void Configure(
        IEnumTypeDescriptor<Domain.Enums.UserStatus> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(ItemName);

        descriptor.BindValuesImplicitly();
    }
}