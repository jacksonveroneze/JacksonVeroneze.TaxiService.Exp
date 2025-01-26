using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Application.v1.Models.User;
using TaxiService.Modules.Passengers.Application.v1.Queries.User;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Enums;
using TaxiService.Modules.Passengers.Domain.Filters;
using TaxiService.Modules.Passengers.Domain.Util;
using TaxiService.Modules.Passengers.Domain.Util.Operators;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.QueryHandler.User;

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

        UserPagedQuery filter = FactoryFilter(request);

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

    private UserPagedQuery FactoryFilter(
        GetUserPagedQuery request)
    {
        PaginationParameters pagination = mapper
            .Map<PaginationParameters>(request);

        UserPagedQuery filter = new()
        {
            Pagination = pagination,
            Filter = new UserPagedFilter()
            {
                NameFilter = StringFilter.Create(request.Name, FilterOperator.Contains),
                StatusFilter = EnumFilter<UserStatus>.Create(request.Status, FilterOperator.Neq),
                StartBirthdayFilter = DateOnlyFilter.Create(request.StartBirthday, FilterOperator.Lte),
                EndBirthdayFilter = DateOnlyFilter.Create(request.EndBirthday, FilterOperator.Gt),
            },
        };

        return filter;
    }
}