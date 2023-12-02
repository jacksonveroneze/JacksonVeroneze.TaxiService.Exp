using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User;

public sealed class CreateUserCommandHandler(
    ILogger<CreateUserCommandHandler> logger,
    IMapper mapper,
    IUserReadRepository readRepository,
    IUserWriteRepository writeRepository)
    : IRequestHandler<CreateUserCommand, Result<CreateUserCommandResponse>>
{
    public async Task<Result<CreateUserCommandResponse>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        bool existsUser = await readRepository
            .ExistsAsync(request.Document!, cancellationToken);

        if (existsUser)
        {
            logger.LogAlreadyExists(nameof(CreateUserCommandHandler),
                nameof(Handle), request.Document!,
                DomainErrors.User.DuplicateCpf);

            return Result<CreateUserCommandResponse>.FromInvalid(
                DomainErrors.User.DuplicateCpf);
        }

        Result<NameValueObject> name = NameValueObject.Create(request.Name!);
        Result<CpfValueObject> cpf = CpfValueObject.Create(request.Document!);

        Result resultValidate = Result.FailuresOrSuccess(name, cpf);

        if (resultValidate.IsFailure)
        {
            logger.LogGenericError(nameof(CreateUserCommandHandler),
                nameof(Handle), resultValidate.Errors!.Count());

            return Result<CreateUserCommandResponse>
                .FromInvalid(resultValidate.Errors!);
        }

        UserEntity entity = new(name.Value!,
            request.Birthday!.Value, request.Gender!.Value,
            cpf.Value!);

        await writeRepository.CreateAsync(
            entity, cancellationToken);

        CreateUserCommandResponse response =
            mapper.Map<CreateUserCommandResponse>(entity);

        logger.LogCreated(nameof(CreateUserCommandHandler),
            nameof(Handle), entity.Id);

        return Result<CreateUserCommandResponse>.WithSuccess(response);
    }
}
