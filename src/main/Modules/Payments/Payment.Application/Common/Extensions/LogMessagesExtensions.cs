using TaxiService.Modules.Payment.Application.v1.Payment.Request;
using TaxiService.Modules.Payment.Domain.Enums;

namespace TaxiService.Modules.Payment.Application.Common.Extensions;

internal static partial class LogMessagesExtensions
{
    #region CommonProcessed

    [LoggerMessage(
        EventId = 1031,
        Level = LogLevel.Information,
        Message = "{contextName} - " +
                  $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  "Identifier: '{identifier}' - Start Pay")]
    internal static partial void LogStartPay(this ILogger logger,
        string contextName, string identifier);

    [LoggerMessage(
        EventId = 1032,
        Level = LogLevel.Information,
        Message = "{contextName} - " +
                  $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)}  - " +
                  "Identifier: '{identifier}' - IsSuccess: '{isSuccess}'- End document signing")]
    internal static partial void LogEndPay(this ILogger logger,
        string contextName, string identifier, bool isSuccess);


    [LoggerMessage(
        EventId = 1033,
        Level = LogLevel.Information,
        Message = "{contextName} - " +
                  $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  "Fetching signature providers.")]
    internal static partial void LogFetchingProviders(this ILogger logger,
        string contextName);


    [LoggerMessage(
        EventId = 1034,
        Level = LogLevel.Error,
        Message = "{contextName} - " +
                  $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  "No signature providers available.")]
    internal static partial void LogNoProvidersAvailable(this ILogger logger,
        string contextName);

    [LoggerMessage(
        EventId = 1035,
        Level = LogLevel.Error,
        Message = "{contextName} - " +
                  $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  "All providers failed to sign the document.")]
    internal static partial void LogFailPayAllProviders(this ILogger logger, 
        string contextName);


    [LoggerMessage(
        EventId = 1036,
        Level = LogLevel.Information,
        Message = "{contextName} - " +
                  $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  "Provider: '{provider}'-  Attempting to sign document with provider")]
    internal static partial void LogStartPayProvider(this ILogger logger,
        string contextName, PaymentProviderType provider);

    [LoggerMessage(
        EventId = 1037,
        Level = LogLevel.Information,
        Message = "{contextName} - " +
                  $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  "Provider: '{provider}'- Document signed successfully")]
    internal static partial void LogSuccessPayProvider(this ILogger logger,
        string contextName, PaymentProviderType provider);


    [LoggerMessage(
        EventId = 1038,
        Level = LogLevel.Warning,
        Message = "{contextName} - " +
                  $"{nameof(RequestCommandHandler)} - " +
                  $"{nameof(RequestCommandHandler.Handle)} - " +
                  "Provider: '{provider}' - Failed to sign the document")]
    internal static partial void LogFailedPayProvider(this ILogger logger,
        string contextName, PaymentProviderType provider);

    #endregion
}
