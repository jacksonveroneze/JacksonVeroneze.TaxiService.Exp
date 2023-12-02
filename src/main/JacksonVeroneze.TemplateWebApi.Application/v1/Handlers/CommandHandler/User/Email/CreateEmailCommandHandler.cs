using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User.Email;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User.Email;

public sealed class CreateEmailCommandHandler(
    ILogger<CreateEmailCommandHandler> logger,
    IMapper mapper,
    IUserReadRepository readRepository,
    IUserWriteRepository writeRepository)
    : IRequestHandler<CreateEmailCommand, Result<CreateEmailCommandResponse>>
{
    public async Task<Result<CreateEmailCommandResponse>> Handle(
        CreateEmailCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            logger.LogNotFound(nameof(CreateEmailCommandHandler),
                nameof(Handle), request.Id, DomainErrors.User.NotFound);

            return Result<CreateEmailCommandResponse>
                .FromNotFound(DomainErrors.User.NotFound);
        }

        Result<EmailValueObject> resultEmailVo = EmailValueObject
            .Create(request.Body!.Email!);

        if (resultEmailVo.IsFailure)
        {
            logger.LogGenericError(nameof(CreateEmailCommandHandler),
                nameof(Handle), request.Id, resultEmailVo.Error!);

            return Result<CreateEmailCommandResponse>
                .FromInvalid(resultEmailVo.Error!);
        }

        EmailEntity email = new(entity, resultEmailVo.Value!);

        Result result = entity.AddEmail(email);

        if (result.IsFailure)
        {
            logger.LogGenericError(nameof(CreateEmailCommandHandler),
                nameof(Handle), request.Id, result.Error!);

            return Result<CreateEmailCommandResponse>
                .FromInvalid(result.Error!);
        }

        await writeRepository.UpdateAsync(entity, cancellationToken);

        CreateEmailCommandResponse response =
            mapper.Map<CreateEmailCommandResponse>(email);

        logger.LogProcessed(nameof(CreateEmailCommandHandler),
            nameof(Handle), entity.Id);

        return Result<CreateEmailCommandResponse>
            .WithSuccess(response);
    }
}
