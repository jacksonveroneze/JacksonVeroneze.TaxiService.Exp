using TaxiService.Modules.Passengers.Application.Models.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Models.Transaction;

public sealed record GetByRideIdQueryResponse :
    DataResponse<TransactionResponse>;