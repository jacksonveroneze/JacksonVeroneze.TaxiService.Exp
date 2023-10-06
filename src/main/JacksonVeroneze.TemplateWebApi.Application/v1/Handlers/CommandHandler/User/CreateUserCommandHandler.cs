using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User;

public sealed class CreateUserCommandHandler :
    IRequestHandler<CreateUserCommand, IResult<CreateUserCommandResponse>>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    public CreateUserCommandHandler(
        ILogger<CreateUserCommandHandler> logger,
        IMapper mapper,
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IResult<CreateUserCommandResponse>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IResult<NameValueObject> name = NameValueObject.Create(request.Name!);
        IResult<CpfValueObject> cpf = CpfValueObject.Create(request.Document!);

        IResult resultValidate = Result.FailuresOrSuccess(name, cpf);

        if (resultValidate.IsFailure)
        {
            _logger.LogGenericError(nameof(CreateUserCommandHandler),
                nameof(Handle), resultValidate.Errors!.Count());

            return Result<CreateUserCommandResponse>
                .Invalid(resultValidate.Errors!);
        }

        bool existsUser = await _readRepository
            .ExistsUserAsync(cpf.Value!.Value!, cancellationToken);

        if (existsUser)
        {
            _logger.LogAlreadyExists(nameof(CreateUserCommandHandler),
                nameof(Handle), DomainErrors.User.DuplicateCpf,
                request.Document!);

            return Result<CreateUserCommandResponse>.Invalid(
                DomainErrors.User.DuplicateCpf);
        }

        UserEntity entity = new UserEntity(name.Value!,
            request.Birthday!.Value, request.Gender!.Value,
            cpf.Value!);

        await _writeRepository.CreateAsync(
            entity, cancellationToken);

        CreateUserCommandResponse response =
            _mapper.Map<CreateUserCommandResponse>(entity);

        _logger.LogCreated(nameof(CreateUserCommandHandler),
            nameof(Handle), entity.Id);

        return Result<CreateUserCommandResponse>.Success(response);
    }
}
