using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Common.BaseApplication.Validators;
using TaxiService.Modules.Driver.Application.Common.Interfaces;
using TaxiService.Modules.Driver.Domain.Core.Errors;
using TaxiService.Modules.Driver.Domain.Repositories;
using IDriverReadRepository =
    TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories.IDriverReadRepository;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Create;

public sealed class CreateDriverCommandHandler(
    ILogger<CreateDriverCommandHandler> logger,
    IEnumerable<IValidator<CreateDriverCommand>> validators,
    IDataMapper mapper,
    IDriverReadRepository driverReadRepository,
    IDriverRepository driverRepository,
    IUnitOfWork unitOfWork,
    IDomainEventDispatcher eventDispatcher)
    : IRequestHandler<CreateDriverCommand, Result<CreateDriverCommandResponse>>
{
    public async Task<Result<CreateDriverCommandResponse>> Handle(
        CreateDriverCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        (bool hasErrors, Error[] errors) = ValidateHelper
            .ValidateRequest(request, validators);

        if (hasErrors)
        {
            return Result<CreateDriverCommandResponse>
                .FromInvalid(errors);
        }

        Result resultVerifyEmailOrDocument =
            await ExistByEmailOrDocumentAsync(
                request.Email!, cancellationToken);

        if (resultVerifyEmailOrDocument.IsFailure)
        {
            return Result<CreateDriverCommandResponse>
                .WithError(resultVerifyEmailOrDocument.FirstError!);
        }

        Result<Domain.Aggregates.Driver> resultEntity = FactoryEntity(request);

        if (resultEntity.IsFailure)
        {
            return Result<CreateDriverCommandResponse>
                .FromInvalid(resultEntity.Errors);
        }

        await CreateAsync(resultEntity.Value!,
            cancellationToken);

        await eventDispatcher.DispatchAsync(resultEntity.Value!);

        CreateDriverCommandResponse response =
            mapper.Map<CreateDriverCommandResponse>(resultEntity.Value);

        return Result<CreateDriverCommandResponse>.WithSuccess(response);
    }

    private async Task<Result> ExistByEmailOrDocumentAsync(
        string email,
        CancellationToken cancellationToken)
    {
        bool existsEmail = await driverReadRepository
            .ExistsByEmailAsync(email, cancellationToken);

        if (!existsEmail)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.DriverErrors.DuplicateEmailOrDocument;

        logger.LogAlreadyExists(nameof(CreateDriverCommandHandler),
            nameof(ExistByEmailOrDocumentAsync), email, error);

        return Result.FromRuleViolation(error);
    }

    private Result<Domain.Aggregates.Driver> FactoryEntity(
        CreateDriverCommand request)
    {
        Result<Domain.Aggregates.Driver> entity = Domain.Aggregates.Driver.Create(
            request.FullName, request.Birthday!.Value,
            request.Email!, request.Phone!,
            request.Document, request.Gender!.Value);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreateDriverCommandHandler),
            nameof(FactoryEntity), entity.Errors.Count);

        return entity;
    }

    private async Task CreateAsync(
        Domain.Aggregates.Driver driver,
        CancellationToken cancellationToken)
    {
        await driverRepository.CreateAsync(
            driver, cancellationToken);

        await unitOfWork.CommitAsync(
            cancellationToken);

        logger.LogCreated(nameof(CreateDriverCommandHandler),
            nameof(CreateAsync), driver.Id);
    }
}
