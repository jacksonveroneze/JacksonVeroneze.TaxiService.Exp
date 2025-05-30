using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Common.BaseApplication.Validators;
using TaxiService.Modules.Drivers.Application.Common.Interfaces;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Create;

public sealed class CreateDriverCommandHandler(
    ILogger<CreateDriverCommandHandler> logger,
    IEnumerable<IValidator<CreateDriverCommand>> validators,
    IDataMapper mapper,
    IDriverReadRepository driverReadRepository,
    IDriverRepository driverRepository,
    IUnitOfWork unitOfWork)
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

        Result<DriverEntity> userEntity = FactoryEntity(request);

        if (userEntity.IsFailure)
        {
            return Result<CreateDriverCommandResponse>
                .FromInvalid(userEntity.Errors);
        }

        await CreateAsync(userEntity.Value!,
            cancellationToken);

        CreateDriverCommandResponse response =
            mapper.Map<CreateDriverCommandResponse>(userEntity.Value);

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

    private Result<DriverEntity> FactoryEntity(
        CreateDriverCommand request)
    {
        Result<DriverEntity> entity = DriverEntity.Create(
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
        DriverEntity driverEntity,
        CancellationToken cancellationToken)
    {
        await driverRepository.CreateAsync(
            driverEntity, cancellationToken);

        await unitOfWork.CommitAsync(
            cancellationToken);

        logger.LogCreated(nameof(CreateDriverCommandHandler),
            nameof(CreateAsync), driverEntity.Id);
    }
}
