using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;

public sealed record AcceptRideCommand :
    IRequest<Result<VoidResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }

    [FromBody]
    public BodyCommand? Body { get; init; }

    public class BodyCommand
    {
        [JsonPropertyName("driver_id")]
        public Guid DriverId { get; init; }
    }
}
