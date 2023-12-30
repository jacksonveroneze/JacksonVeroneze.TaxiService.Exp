using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.User.Email;

public sealed class CreateEmailCommandHandler(
    ILogger<CreateEmailCommandHandler> logger,
    IMapper mapper,
    IUserReadRepository userReadRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository)
    : IRequestHandler<CreateEmailCommand, Result<CreateEmailCommandResponse>>
{
    public async Task<Result<CreateEmailCommandResponse>> Handle(
        CreateEmailCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? user = await userReadRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result<CreateEmailCommandResponse>.FromNotFound(
                DomainErrors.UserError.NotFound);
        }

        bool existsEmail = await emailReadRepository
            .ExistsAsync(request.Body!.Email!, cancellationToken);

        if (existsEmail)
        {
            logger.LogGenericError(nameof(CreateEmailCommandHandler),
                nameof(Handle), request.Id,
                DomainErrors.EmailError.DuplicateEmail);

            return Result<CreateEmailCommandResponse>
                .FromInvalid(DomainErrors.EmailError.DuplicateEmail);
        }

        Result<EmailEntity> entity = EmailEntity
            .Create(user.Id, request.Body!.Email);

        if (entity.IsFailure)
        {
            logger.LogGenericError(nameof(CreateEmailCommandHandler),
                nameof(Handle), request.Id, entity.Error!);

            return Result<CreateEmailCommandResponse>
                .FromInvalid(entity.Error!);
        }

        await emailWriteRepository.CreateAsync(
            entity.Value!, cancellationToken);

        CreateEmailCommandResponse response =
            mapper.Map<CreateEmailCommandResponse>(entity.Value);

        logger.LogProcessed(nameof(CreateEmailCommandHandler),
            nameof(Handle), entity.Value!.Id);

        return Result<CreateEmailCommandResponse>
            .WithSuccess(response);
    }
}