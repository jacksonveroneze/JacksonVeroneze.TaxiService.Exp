using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Queries.Ride;

public sealed record GetRidePagedQuery :
    PagedQuery, IRequest<IResult<GetRidePagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(RideResponse.Id);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    [FromQuery(Name = "status")]
    public RideStatus? Status { get; init; }

    public GetRidePagedQuery() : base(DefaultOrderBy, DefaultOrder)
    {
    }
}
