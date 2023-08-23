using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.Bank;

internal sealed  class InactivateBankCommandHandler :
    IRequestHandler<InactivateBankCommand, IResult<VoidResponse>>
{
    private readonly ILogger<InactivateBankCommandHandler> _logger;
    private readonly IBankReadRepository _readRepository;
    private readonly IBankWriteRepository _writeRepository;

    public InactivateBankCommandHandler(
        ILogger<InactivateBankCommandHandler> logger,
        IBankReadRepository readRepository,
        IBankWriteRepository writeRepository)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
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

        if (data.Status is not BankStatus.Active)
        {
            _logger.AlreadyProcessed(nameof(InactivateBankCommandHandler),
                nameof(Handle), request.Id);

            return Result<VoidResponse>.Invalid(
                DomainErrors.Bank.AlreadyProcessed);
        }

        data.Inactivate();

        await _writeRepository.UpdateAsync(data, cancellationToken);

        _logger.LogInactivated(nameof(InactivateBankCommandHandler),
            nameof(Handle), request.Id);

        return Result<VoidResponse>.Success();
    }
}
