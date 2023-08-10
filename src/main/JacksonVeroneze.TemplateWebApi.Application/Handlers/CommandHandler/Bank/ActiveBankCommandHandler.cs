using Ardalis.Result;
using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.Bank;

public class ActivateBankCommandHandler :
    IRequestHandler<ActivateBankCommand, Result<BaseResponse>>
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

    public async Task<Result<BaseResponse>> Handle(
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

            return Result.NotFound();
        }

        if (data.Status is not BankStatus.PendingActivation)
        {
            return Result.Error("Item já ativado");
        }

        data.Activate();

        await _writeRepository.UpdateAsync(data, cancellationToken);

        _logger.LogActivated(nameof(ActivateBankCommandHandler),
            nameof(Handle), request.Id);

        return Result.Success();
    }
}
