using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;

public class UserStatusType : EnumType<UserStatus>
{
    protected override void Configure(
        IEnumTypeDescriptor<UserStatus> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(nameof(UserStatusType));

        descriptor.BindValuesImplicitly();
    }
}