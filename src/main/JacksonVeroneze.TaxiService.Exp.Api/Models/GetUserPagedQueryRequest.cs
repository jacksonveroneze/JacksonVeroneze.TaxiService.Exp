using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.Models;

public sealed record GetUserPagedQueryRequest
{
    [FromQuery(Name = "name")]
    public string? Name { get; init; }

    // [FromQuery(Name = "order_by")]
    // public OrderDto?[]? OrderBY { get; init; }
}