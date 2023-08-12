using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.Bank;

public class DeleteBankCommandHandler :
    IRequestHandler<DeleteBankCommand, Result<BaseResponse>>
{
    private readonly ILogger<DeleteBankCommandHandler> _logger;
    private readonly IBankReadRepository _readRepository;
    private readonly IBankWriteRepository _writeRepository;

    public DeleteBankCommandHandler(
        ILogger<DeleteBankCommandHandler> logger,
        IBankReadRepository readRepository,
        IBankWriteRepository writeRepository)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<Result<BaseResponse>> Handle(
        DeleteBankCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        BankEntity? data = await _readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (data is null)
        {
            _logger.LogNotFound(nameof(DeleteBankCommandHandler),
                nameof(Handle), request.Id);

            return Result<BaseResponse>.NotFound(DomainErrors.Bank.NotFound);
        }

        await _writeRepository.DeleteAsync(data, cancellationToken);

        _logger.LogDeleted(nameof(DeleteBankCommandHandler),
            nameof(Handle), request.Id.ToString());

        return Result<BaseResponse>.Success();
    }
}
