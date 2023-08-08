using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.Bank;

public class ActivateBankCommandHandler :
    IRequestHandler<ActivateBankCommand, BaseResponse>
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

    public async Task<BaseResponse> Handle(
        ActivateBankCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        BankEntity? data = await _readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (data is null)
        {
            _logger.LogNotFound(nameof(ActivateBankCommandHandler),
                nameof(Handle), request.Id.ToString());

            return new BankNotFoundResponse(request.Id.ToString());
        }

        if (data.Status is not BankStatus.PendingActivation)
        {
            return new BadRequestResponse(request.Id.ToString(), "Item já ativado");
        }

        data.Activate();

        await _writeRepository.UpdateAsync(data, cancellationToken);

        ActivateBankCommandResponse response = new();

        _logger.LogActivated(nameof(ActivateBankCommandHandler),
            nameof(Handle), request.Id);

        return response;
    }
}
