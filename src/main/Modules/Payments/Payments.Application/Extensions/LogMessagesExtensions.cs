using TaxiService.Modules.Payments.Application.v1.Handlers.CommandHandler.Payment;
using TaxiService.Modules.Payments.Domain.Enums;

namespace TaxiService.Modules.Payments.Application.Extensions;

internal static partial class LogMessagesExtensions
{
    #region CommonProcessed

    [LoggerMessage(
        EventId = 1031,
        Level = LogLevel.Information,
        Message = $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - Start signing - " +
                  "Identifier: '{identifier}'")]
    internal static partial void LogStartSignature(this ILogger logger,
        string identifier);

    [LoggerMessage(
        EventId = 1032,
        Level = LogLevel.Information,
        Message = $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - End document signing - " +
                  "Identifier: '{identifier}' - IsSuccess: '{isSuccess}'")]
    internal static partial void LogEndSignature(this ILogger logger,
        string identifier, bool isSuccess);
    
    
    [LoggerMessage(
        EventId = 1033,
        Level = LogLevel.Information,
        Message = $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  $"Fetching signature providers.")]
    internal static partial void LogFetchingProviders(this ILogger logger);
    
    
    [LoggerMessage(
        EventId = 1034,
        Level = LogLevel.Error,
        Message = $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  $"No signature providers available.")]
    internal static partial void LogNoProvidersAvailable(this ILogger logger);
    
    [LoggerMessage(
        EventId = 1035,
        Level = LogLevel.Error,
        Message = $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  $"All providers failed to sign the document.")]
    internal static partial void LogFailSignatureAllProviders(this ILogger logger);
    
    
    [LoggerMessage(
        EventId = 1036,
        Level = LogLevel.Information,
        Message = $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  $"Attempting to sign document with provider - " +
                  "Provider: '{provider}'")]
    internal static partial void LogStartSignatureProvider(this ILogger logger,
        PaymentProviderType provider);
    
    [LoggerMessage(
        EventId = 1037,
        Level = LogLevel.Information,
        Message = $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  $"Document signed successfully - " +
                  "Provider: '{provider}'")]
    internal static partial void LogSuccessSignatureProvider(this ILogger logger,
        PaymentProviderType provider);
    
    
    [LoggerMessage(
        EventId = 1038,
        Level = LogLevel.Warning,
        Message = $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  $"Failed to sign the document - " +
                  "Provider: '{provider}'")]
    internal static partial void LogFailedSignatureProvider(this ILogger logger,
        PaymentProviderType provider);
    
    
    #endregion
}