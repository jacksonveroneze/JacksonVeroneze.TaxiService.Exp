using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User.Email;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.QueryHandler.User.Email;

public sealed class GetAllEmailsByUserIdQueryHandler(
    ILogger<GetAllEmailsByUserIdQueryHandler> logger,
    IMapper mapper,
    IUserReadRepository repository)
    : IRequestHandler<GetAllEmailsByUserIdQuery, Result<GetAllEmailsByUserIdQueryResponse>>
{
    public async Task<Result<GetAllEmailsByUserIdQueryResponse>> Handle(
        GetAllEmailsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            logger.LogNotFound(nameof(GetAllEmailsByUserIdQueryHandler),
                nameof(Handle), request.Id, DomainErrors.User.NotFound);

            return Result<GetAllEmailsByUserIdQueryResponse>.FromNotFound(
                DomainErrors.User.NotFound);
        }

        IReadOnlyCollection<EmailEntity> emails = entity.Emails;

        GetAllEmailsByUserIdQueryResponse response =
            mapper.Map<GetAllEmailsByUserIdQueryResponse>(emails);

        logger.LogGetPaged(nameof(GetAllEmailsByUserIdQueryHandler),
            nameof(Handle), 1, emails.Count, emails.Count);

        return Result<GetAllEmailsByUserIdQueryResponse>
            .WithSuccess(response);
    }
}