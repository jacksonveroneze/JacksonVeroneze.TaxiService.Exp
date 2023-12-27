using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;

public sealed record GetRideByIdQueryResponse :
    DataResponse<RideResponse>;