using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passenger.Email;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.QueryHandler.Email;

public sealed class GetEmailPagedQueryHandler(
    ILogger<GetEmailPagedQueryHandler> logger,
    IDataMapper mapper,
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

        ICollection<EmailEntity> all = await emailReadRepository
            .GetAllAsync(filter, cancellationToken);

        GetEmailPagedQueryResponse response =
            mapper.Map<GetEmailPagedQueryResponse>(all);

        logger.LogGetAll(
            nameof(GetEmailPagedQueryHandler),
            nameof(Handle), all.Count);

        return Result<GetEmailPagedQueryResponse>
            .WithSuccess(response);
    }
}
