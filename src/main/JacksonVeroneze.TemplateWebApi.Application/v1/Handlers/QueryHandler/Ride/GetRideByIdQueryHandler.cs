using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.QueryHandler.Ride;

public sealed class GetRideByIdQueryHandler(
    ILogger<GetRideByIdQueryHandler> logger,
    IMapper mapper,
    IRideReadRepository repository)
    : IRequestHandler<GetRideByIdQuery, Result<GetRideByIdQueryResponse>>
{
    public async Task<Result<GetRideByIdQueryResponse>> Handle(
        GetRideByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RideEntity? entity = await repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<GetRideByIdQueryResponse>
                .FromNotFound(DomainErrors.Ride.NotFound);
        }

        GetRideByIdQueryResponse response =
            mapper.Map<GetRideByIdQueryResponse>(entity);

        logger.LogGetById(nameof(GetRideByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetRideByIdQueryResponse>
            .WithSuccess(response);
    }
}
