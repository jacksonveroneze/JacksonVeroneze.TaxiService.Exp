using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Common;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.Bank;

internal sealed  class InactivateBankCommandHandler :
    IRequestHandler<InactivateBankCommand, IResult<VoidResponse>>
{
    private readonly ILogger<InactivateBankCommandHandler> _logger;
    private readonly IBankReadRepository _readRepository;
    private readonly IBankWriteRepository _writeRepository;
    private readonly IDateTime _dateTime;

    public InactivateBankCommandHandler(
        ILogger<InactivateBankCommandHandler> logger,
        IBankReadRepository readRepository,
        IBankWriteRepository writeRepository,
        IDateTime dateTime)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _dateTime = dateTime;
    }

    public async Task<IResult<VoidResponse>> Handle(
        InactivateBankCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        BankEntity? data = await _readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (data is null)
        {
            _logger.LogNotFound(nameof(InactivateBankCommandHandler),
                nameof(Handle), request.Id);

            return Result<VoidResponse>.NotFound(
                DomainErrors.Bank.NotFound);
        }

        IResult result = data.Inactivate(_dateTime.UtcNow);

        if (result.Status != ResultStatus.Success)
        {
            _logger.AlreadyProcessed(nameof(ActivateBankCommandHandler),
                nameof(Handle), request.Id);

            return Result<VoidResponse>.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(data, cancellationToken);

        _logger.LogInactivated(nameof(InactivateBankCommandHandler),
            nameof(Handle), request.Id);

        return Result<VoidResponse>.Success();
    }
}