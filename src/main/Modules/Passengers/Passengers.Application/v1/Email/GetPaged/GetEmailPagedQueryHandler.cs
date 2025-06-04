using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Passengers.Application.v1.Email.Common.Filters;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Filters;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Email.GetPaged;

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

        ICollection<Domain.Entities.Email> all = await emailReadRepository
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
