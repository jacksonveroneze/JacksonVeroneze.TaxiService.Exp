using TaxiService.Modules.Passengers.Application.Models.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Models.User;

public sealed record CreateUserCommandResponse
    : DataResponse<UserResponse>;