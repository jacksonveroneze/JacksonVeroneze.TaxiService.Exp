using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Util;
using JacksonVeroneze.TaxiService.Exp.Domain.Util.Operators;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.QueryHandler.User;

public sealed class GetUserPagedQueryHandler(
    ILogger<GetUserPagedQueryHandler> logger,
    IMapper mapper,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetUserPagedQuery, Result<GetUserPagedQueryResponse>>
{
    public async Task<Result<GetUserPagedQueryResponse>> Handle(
        GetUserPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserPagedFilter filter = FactoryFilter(request);

        Page<UserEntity> page = await userReadRepository
            .GetPagedAsync(filter, cancellationToken);

        GetUserPagedQueryResponse response =
            mapper.Map<GetUserPagedQueryResponse>(page);

        logger.LogGetPagedWithCursor(
            nameof(GetUserPagedQueryHandler),
            nameof(Handle), request.PageSize!.Value,
            page.Data.Count, page.PageInfo.HasMore);

        return Result<GetUserPagedQueryResponse>
            .WithSuccess(response);
    }

    private UserPagedFilter FactoryFilter(
        GetUserPagedQuery request)
    {
        PaginationParameters pagination = mapper
            .Map<PaginationParameters>(request);

        UserPagedFilter filter = new()
        {
            Pagination = pagination,
            NameFilter = request.NameFilter,
            StatusFilter = EnumFilter<UserStatus>.Create(request.Status),
            StartBirthdayFilter =
                DateOnlyFilter.Create(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-20)),
                    DateOnlyOperator.Lt),
            EndBirthdayFilter =
                DateOnlyFilter.Create(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)),
                    DateOnlyOperator.Gte)
        };

        return filter;
    }
}