using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Payments.Application.Extensions;
using TaxiService.Modules.Payments.Application.v1.Commands.Payment;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Factories;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Providers;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Repositories.Payment;
using TaxiService.Modules.Payments.Domain.Core.Errors;
using TaxiService.Modules.Payments.Domain.Entities;
using TaxiService.Modules.Payments.Domain.Models;

namespace TaxiService.Modules.Payments.Application.v1.Handlers.CommandHandler.Payment;

public sealed class RequestCommandHandler(
    ILogger<RequestCommandHandler> logger,
    IPaymentProviderFactory providerFactory,
    IPaymentWriteRepository writeRepository)
    : IRequestHandler<RequestCommand, Result>
{
    public async Task<Result> Handle(
        RequestCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogStartSignature(request.RideId.ToString());

        var document = new PaymentEntity();

        await writeRepository
            .CreateAsync(document, cancellationToken);

        var result = await TrySignAsync(
            document, cancellationToken);

        logger.LogEndSignature(
            request.RideId.ToString(), result.IsSuccess);

        return result;
    }

    private async Task<Result> TrySignAsync(
        PaymentEntity payment,
        CancellationToken cancellationToken)
    {
        logger.LogFetchingProviders();

        var providers = await providerFactory
            .GetProvidersAsync(cancellationToken);

        if (!providers.Any())
        {
            logger.LogNoProvidersAvailable();

            return Result.WithError(
                DomainErrors.ProviderError.Unavailable);
        }

        foreach (var provider in providers)
        {
            var result = await AttemptSignatureWithProviderAsync(
                provider.Value, payment, cancellationToken);

            if (!result.IsSuccess)
            {
                continue;
            }

            payment.SetCompleted();

            await writeRepository.UpdateAsync(
                payment, cancellationToken);

            return result;
        }

        logger.LogFailSignatureAllProviders();

        return Result.WithError(
            DomainErrors.PaymentError.SigningError);
    }

    private async Task<Result<PaymentResult>> AttemptSignatureWithProviderAsync(
        IPaymentProvider provider,
        PaymentEntity document,
        CancellationToken cancellationToken)
    {
        logger.LogStartSignatureProvider(provider.Identifier);

        var result = await provider.PayAsync(
            document, cancellationToken);

        if (result.Success)
        {
            logger.LogSuccessSignatureProvider(
                provider.Identifier);

            return Result<PaymentResult>.WithSuccess(result);
        }

        logger.LogFailedSignatureProvider(provider.Identifier);

        return Result<PaymentResult>.WithError(
            DomainErrors.PaymentError.SigningErrorProvider);
    }
}
