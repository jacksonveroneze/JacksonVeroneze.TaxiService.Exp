using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User.Email;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User.Email;

public sealed class CreateEmailCommandHandler :
    IRequestHandler<CreateEmailCommand, IResult<CreateEmailCommandResponse>>
{
    private readonly ILogger<CreateEmailCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    public CreateEmailCommandHandler(
        ILogger<CreateEmailCommandHandler> logger,
        IMapper mapper,
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IResult<CreateEmailCommandResponse>> Handle(
        CreateEmailCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await _readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            _logger.LogNotFound(nameof(CreateEmailCommandHandler),
                nameof(Handle), request.Id, DomainErrors.User.NotFound);

            return Result<CreateEmailCommandResponse>
                .NotFound(DomainErrors.User.NotFound);
        }

        IResult<EmailValueObject> resultEmailVo = EmailValueObject
            .Create(request.Body!.Email!);

        if (resultEmailVo.IsFailure)
        {
            _logger.LogGenericError(nameof(CreateEmailCommandHandler),
                nameof(Handle), resultEmailVo.Error!, request.Id.ToString());

            return Result<CreateEmailCommandResponse>
                .Invalid(resultEmailVo.Error!);
        }

        EmailEntity email = new(entity, resultEmailVo.Value!);

        IResult result = entity.AddEmail(email);

        if (result.IsFailure)
        {
            _logger.LogGenericError(nameof(CreateEmailCommandHandler),
                nameof(Handle), result.Error!, request.Id.ToString());

            return Result<CreateEmailCommandResponse>
                .Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(entity, cancellationToken);

        CreateEmailCommandResponse response =
            _mapper.Map<CreateEmailCommandResponse>(email);

        _logger.LogProcessed(nameof(CreateEmailCommandHandler),
            nameof(Handle), entity.Id);

        return Result<CreateEmailCommandResponse>
            .Success(response);
    }
}
