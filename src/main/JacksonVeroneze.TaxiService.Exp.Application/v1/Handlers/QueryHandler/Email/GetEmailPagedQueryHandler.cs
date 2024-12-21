using JacksonVeroneze.NET.Pagination.Offset;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User.Email;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.QueryHandler.Email;

public sealed class GetEmailPagedQueryHandler(
    ILogger<GetEmailPagedQueryHandler> logger,
    IMapper mapper,
    IEmailReadRepository emailReadRepository)
    : IRequestHandler<GetEmailPagedQuery, Result<GetEmailPagedQueryResponse>>
{
    public async Task<Result<GetEmailPagedQueryResponse>> Handle(
        GetEmailPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        EmailPagedFilter filter = mapper
            .Map<EmailPagedFilter>(request);

        Page<EmailEntity> page = await emailReadRepository
            .GetPagedAsync(filter, cancellationToken);

        GetEmailPagedQueryResponse response =
            mapper.Map<GetEmailPagedQueryResponse>(page);

        logger.LogGetPagedWithOffset(
            nameof(GetEmailPagedQueryHandler),
            nameof(Handle), request.Page!.Value,
            request.PageSize!.Value,
            page.PageInfo.TotalElements);

        return Result<GetEmailPagedQueryResponse>
            .WithSuccess(response);
    }
}