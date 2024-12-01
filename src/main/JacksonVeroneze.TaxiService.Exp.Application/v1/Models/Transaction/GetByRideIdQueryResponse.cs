using JacksonVeroneze.TaxiService.Exp.Application.Models.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Transaction;

public sealed record GetByRideIdQueryResponse :
    DataResponse<TransactionResponse>;