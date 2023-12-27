using JacksonVeroneze.NET.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Base;

public abstract record PagedQuery
{
    private const int DefaultPage = 1;

    private const int DefaulPageSize = 20;

    protected PagedQuery(string defaultOrderBy, SortDirection defaultOrder)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(defaultOrderBy));
        ArgumentException.ThrowIfNullOrEmpty(nameof(defaultOrder));

        _orderBy = defaultOrderBy;
        _order = defaultOrder;

        _page = DefaultPage;
        _pageSize = DefaulPageSize;
    }

    private readonly int _page;
    private readonly int _pageSize;
    private readonly string? _orderBy;
    private readonly SortDirection? _order;

    [FromQuery(Name = "page")]
    public int? Page
    {
        get => _page;
        init => _page = value ?? DefaultPage;
    }

    [FromQuery(Name = "page_size")]
    public int? PageSize
    {
        get => _pageSize;
        init => _pageSize = value ?? DefaulPageSize;
    }

    [FromQuery(Name = "order_by")]
    public string? OrderBy
    {
        get => _orderBy;
        init => _orderBy = value ?? _orderBy;
    }

    [FromQuery(Name = "order")]
    public SortDirection? Order
    {
        get => _order;
        init => _order = value ?? _order;
    }
}