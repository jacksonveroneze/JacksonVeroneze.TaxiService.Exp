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
                NameFilter = StringFilter.Create(request.Name, FilterOperator.StartsWith),
                StatusFilter = EnumFilter<UserStatus>.Create(request.Status),
                StartBirthdayFilter = DateOnlyFilter.Create(request.StartBirthday),
                EndBirthdayFilter = DateOnlyFilter.Create(request.EndBirthday),
            },
        };

        return filter;
    }
}