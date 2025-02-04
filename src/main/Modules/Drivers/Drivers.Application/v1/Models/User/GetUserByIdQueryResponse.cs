using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Models.User;

public sealed record GetUserByIdQueryResponse
    : DataResponse<UserResponse>;