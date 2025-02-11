using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passanger.Email;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.QueryHandler.Email;

public sealed class GetPagedQueryHandler(
    ILogger<GetPagedQueryHandler> logger,
    IMapper mapper,
    IEmailReadRepository emailReadRepository)
    : IRequestHandler<GetPagedQuery, Result<GetPagedQueryResponse>>
{
    public async Task<Result<GetPagedQueryResponse>> Handle(
        GetPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        EmailPagedFilter filter = mapper
            .Map<EmailPagedFilter>(request);

        ICollection<EmailEntity> all = await emailReadRepository
            .GetAllAsync(filter, cancellationToken);

        GetPagedQueryResponse response =
            mapper.Map<GetPagedQueryResponse>(all);

        logger.LogGetAll(
            nameof(GetPagedQueryHandler),
            nameof(Handle), all.Count);

        return Result<GetPagedQueryResponse>
            .WithSuccess(response);
    }
}