using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User.Email;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.QueryHandler.User.Email;

public sealed class GetAllEmailsByUserIdQueryHandler(
    ILogger<GetAllEmailsByUserIdQueryHandler> logger,
    IMapper mapper,
    IUserReadRepository userReadRepository,
    IEmailReadRepository emailReadRepository)
    : IRequestHandler<GetEmailsByUserIdPagedQuery, Result<GetEmailsByUserIdQueryResponse>>
{
    public async Task<Result<GetEmailsByUserIdQueryResponse>> Handle(
        GetEmailsByUserIdPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await userReadRepository
            .GetByIdAsync(request.UserId, cancellationToken);

        if (entity is null)
        {
            logger.LogNotFound(nameof(GetAllEmailsByUserIdQueryHandler),
                nameof(Handle), request.UserId, DomainErrors.UserError.NotFound);

            return Result<GetEmailsByUserIdQueryResponse>.FromNotFound(
                DomainErrors.UserError.NotFound);
        }

        EmailPagedFilter filter = mapper
            .Map<EmailPagedFilter>(request);

        ICollection<EmailEntity> page = await emailReadRepository
            .GetPagedAsync(filter, cancellationToken);

        GetEmailsByUserIdQueryResponse response =
            mapper.Map<GetEmailsByUserIdQueryResponse>(page);

        return Result<GetEmailsByUserIdQueryResponse>
            .WithSuccess(response);
    }
}