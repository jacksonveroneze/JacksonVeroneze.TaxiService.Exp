using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.User.Email;

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

        UserEntity? user = await readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result<CreateEmailCommandResponse>.FromInvalid(
                DomainErrors.User.NotFound);
        }

        Result<EmailEntity> entity = EmailEntity
            .Create(user, request.Body!.Email);

        if (entity.IsFailure)
        {
            logger.LogGenericError(nameof(CreateEmailCommandHandler),
                nameof(Handle), request.Id, entity.Error!);

            return Result<CreateEmailCommandResponse>
                .FromInvalid(entity.Error!);
        }

        Result result = user.AddEmail(entity.Value!);

        if (result.IsFailure)
        {
            logger.LogGenericError(nameof(CreateEmailCommandHandler),
                nameof(Handle), request.Id, result.Error!);

            return Result<CreateEmailCommandResponse>
                .FromInvalid(result.Error!);
        }

        await writeRepository.UpdateAsync(user, cancellationToken);

        CreateEmailCommandResponse response =
            mapper.Map<CreateEmailCommandResponse>(entity.Value);

        logger.LogProcessed(nameof(CreateEmailCommandHandler),
            nameof(Handle), entity.Value!.Id);

        return Result<CreateEmailCommandResponse>
            .WithSuccess(response);
    }
}
