using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.Bank;

public class ActivateBankCommandHandler :
    IRequestHandler<ActivateBankCommand, IResult<VoidResponse>>
{
    private readonly ILogger<ActivateBankCommandHandler> _logger;
    private readonly IBankReadRepository _readRepository;
    private readonly IBankWriteRepository _writeRepository;

    public ActivateBankCommandHandler(
        ILogger<ActivateBankCommandHandler> logger,
        IBankReadRepository readRepository,
        IBankWriteRepository writeRepository)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IResult<VoidResponse>> Handle(
        ActivateBankCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        BankEntity? data = await _readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (data is null)
        {
            _logger.LogNotFound(nameof(ActivateBankCommandHandler),
                nameof(Handle), request.Id);

            return Result<VoidResponse>.NotFound(
                DomainErrors.Bank.NotFound);
        }

        if (data.Status is not BankStatus.PendingActivation)
        {
            _logger.AlreadyProcessed(nameof(ActivateBankCommandHandler),
                nameof(Handle), request.Id);

            return Result<VoidResponse>.Invalid(
                DomainErrors.Bank.AlreadyProcessed);
        }

        data.Activate();

        await _writeRepository.UpdateAsync(data, cancellationToken);

        _logger.LogActivated(nameof(ActivateBankCommandHandler),
            nameof(Handle), request.Id);

        return Result<VoidResponse>.Success();
    }
}
