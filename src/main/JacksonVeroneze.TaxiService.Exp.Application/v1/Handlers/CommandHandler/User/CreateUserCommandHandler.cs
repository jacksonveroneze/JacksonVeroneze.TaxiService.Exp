using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.User;

public sealed class CreateUserCommandHandler(
    ILogger<CreateUserCommandHandler> logger,
    IMapper mapper,
    IUserWriteRepository userWriteRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository)
    : IRequestHandler<CreateUserCommand, Result<CreateUserCommandResponse>>
{
    public async Task<Result<CreateUserCommandResponse>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        bool existsUser = await emailReadRepository
            .ExistsAsync(request.Email!, cancellationToken);

        if (existsUser)
        {
            logger.LogAlreadyExists(nameof(CreateUserCommandHandler),
                nameof(Handle), request.Email!,
                DomainErrors.UserError.DuplicateEmail);

            return Result<CreateUserCommandResponse>.FromInvalid(
                DomainErrors.UserError.DuplicateEmail);
        }

        Result<UserEntity> entity = UserEntity.Create(request.Name,
            request.Birthday!.Value, request.Gender!.Value, request.Document);

        if (entity.IsFailure)
        {
            logger.LogGenericError(nameof(CreateUserCommandHandler),
                nameof(Handle), entity.Errors!.Count());

            return Result<CreateUserCommandResponse>
                .FromInvalid(entity.Errors!);
        }

        await userWriteRepository.CreateAsync(
            entity.Value!, cancellationToken);

        Result<EmailEntity> emailEntity = EmailEntity
            .Create(entity.Value!.Id, request.Email!);

        if (emailEntity.IsFailure)
        {
            logger.LogGenericError(nameof(CreateUserCommandHandler),
                nameof(Handle), entity.Error!.Message);
        }
        else
        {
            await emailWriteRepository.CreateAsync(
                emailEntity.Value!, cancellationToken);
        }

        CreateUserCommandResponse response =
            mapper.Map<CreateUserCommandResponse>(entity.Value);

        logger.LogCreated(nameof(CreateUserCommandHandler),
            nameof(Handle), entity.Value!.Id);

        return Result<CreateUserCommandResponse>.WithSuccess(response);
    }
}