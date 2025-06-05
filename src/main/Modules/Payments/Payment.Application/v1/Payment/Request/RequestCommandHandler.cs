using System.Collections.Immutable;
using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Payment.Application.Common.Extensions;
using TaxiService.Modules.Payment.Application.Common.Interfaces.Factories;
using TaxiService.Modules.Payment.Application.Common.Interfaces.Providers;
using TaxiService.Modules.Payment.Domain;
using TaxiService.Modules.Payment.Domain.Core.Errors;
using TaxiService.Modules.Payment.Domain.Models;
using TaxiService.Modules.Payment.Domain.Repositories;

namespace TaxiService.Modules.Payment.Application.v1.Payment.Request;

public sealed class RequestCommandHandler(
    ILogger<RequestCommandHandler> logger,
    IPaymentProviderFactory providerFactory,
    IPaymentRepository writeRepository)
    : IRequestHandler<RequestCommand, Result>
{
    public async Task<Result> Handle(
        RequestCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogStartSignature(
            ModuleConstants.ContextName,
            request.RideId.ToString());

        Domain.Aggregates.Payment document = new();

        await writeRepository
            .CreateAsync(document, cancellationToken);

        Result result = await TryPayAsync(
            document, cancellationToken);

        logger.LogEndSignature(
            ModuleConstants.ContextName,
            request.RideId.ToString(), result.IsSuccess);

        return result;
    }

    private async Task<Result> TryPayAsync(
        Domain.Aggregates.Payment payment,
        CancellationToken cancellationToken)
    {
        logger.LogFetchingProviders(
            ModuleConstants.ContextName);

        ImmutableArray<Lazy<IPaymentProvider>> providers =
            await providerFactory.GetProvidersAsync(cancellationToken);

        if (!providers.Any())
        {
            logger.LogNoProvidersAvailable(
                ModuleConstants.ContextName);

            return Result.WithError(
                DomainErrors.ProviderError.Unavailable);
        }

        foreach (Lazy<IPaymentProvider> provider in providers)
        {
            Result<PaymentResult> result = await AttemptSignatureWithProviderAsync(
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

        logger.LogFailSignatureAllProviders(
            ModuleConstants.ContextName);

        return Result.WithError(
            DomainErrors.PaymentError.PayError);
    }

    private async Task<Result<PaymentResult>> AttemptSignatureWithProviderAsync(
        IPaymentProvider provider,
        Domain.Aggregates.Payment document,
        CancellationToken cancellationToken)
    {
        logger.LogStartSignatureProvider(
            ModuleConstants.ContextName,
            provider.Identifier);

        PaymentResult result = await provider.PayAsync(
            document, cancellationToken);

        if (result.Success)
        {
            logger.LogSuccessSignatureProvider(
                ModuleConstants.ContextName,
                provider.Identifier);

            return Result<PaymentResult>.WithSuccess(result);
        }

        logger.LogFailedSignatureProvider(
            ModuleConstants.ContextName,
            provider.Identifier);

        return Result<PaymentResult>.WithError(
            DomainErrors.PaymentError.PayErrorProvider);
    }
}
