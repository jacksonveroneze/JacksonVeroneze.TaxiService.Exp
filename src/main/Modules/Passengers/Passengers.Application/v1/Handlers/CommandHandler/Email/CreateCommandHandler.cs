using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Email;

public sealed class CreateCommandHandler(
    ILogger<CreateCommandHandler> logger,
    IMapper mapper,
    IPassangerReadRepository passangerReadRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository)
    : IRequestHandler<CreateCommand, Result<CreateCommandResponse>>
{
    public async Task<Result<CreateCommandResponse>> Handle(
        CreateCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassangerEntity> resultPassanger = await GetPassangerByIdAsync(
            request.PassangerId, cancellationToken);

        if (resultPassanger.IsFailure)
        {
            return Result<CreateCommandResponse>.FromNotFound(
                resultPassanger.Error!);
        }

        Result existsEmail = await ExistsEmailAsync(
            request, cancellationToken);

        if (existsEmail.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(existsEmail.Error!);
        }

        Result<EmailEntity> entity = FactoryEntity(
            request, resultPassanger.Value!);

        if (entity.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(entity.Errors!);
        }

        await CreateAsync(entity.Value!, cancellationToken);

        CreateCommandResponse response =
            mapper.Map<CreateCommandResponse>(entity.Value);

        return Result<CreateCommandResponse>.WithSuccess(response);
    }

    private Result<EmailEntity> FactoryEntity(
        CreateCommand request, PassangerEntity passanger)
    {
        Result<EmailEntity> entity = EmailEntity
            .Create(passanger.Id, request.Body!.Email);

        if (!entity.IsFailure)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreateCommandHandler),
            nameof(FactoryEntity), entity.Errors!.Count());

        return Result<EmailEntity>
            .FromInvalid(entity.Errors!);
    }

    private async Task<Result<PassangerEntity>> GetPassangerByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassangerEntity? passanger = await passangerReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (passanger is null)
        {
            return Result<PassangerEntity>.FromNotFound(
                DomainErrors.PassangerError.NotFound);
        }

        return Result<PassangerEntity>.WithSuccess(passanger);
    }

    private async Task<Result> ExistsEmailAsync(
        CreateCommand request,
        CancellationToken cancellationToken)
    {
        bool existsEmail = await emailReadRepository
            .ExistsAsync(request.Body!.Email!, cancellationToken);

        if (!existsEmail)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.EmailError.DuplicateEmail;

        logger.LogGenericError(
            nameof(CreateCommandHandler),
            nameof(Handle), request.PassangerId, error);

        return Result<CreateCommandResponse>
            .FromInvalid(error);
    }

    private async Task CreateAsync(
        EmailEntity email,
        CancellationToken cancellationToken)
    {
        await emailWriteRepository.CreateAsync(
            email, cancellationToken);

        logger.LogProcessed(
            nameof(CreateCommandHandler),
            nameof(CreateAsync), email.Id);
    }
}