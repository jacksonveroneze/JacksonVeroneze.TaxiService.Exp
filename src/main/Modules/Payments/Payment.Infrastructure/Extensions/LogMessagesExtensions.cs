using System.Net;
using Microsoft.Extensions.Logging;
using Refit;
using TaxiService.Modules.Payment.Domain.Enums;
using TaxiService.Modules.Payment.Infrastructure.Factories;

namespace TaxiService.Modules.Payment.Infrastructure.Extensions;

internal static partial class LogMessagesExtensions
{
    #region Common

    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Error,
        Message = "{className} - {methodName} - Error")]
    public static partial void LogGenericError(this ILogger logger,
        string className, string methodName, Exception ex);

    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Error,
        Message = "{className} - {methodName} - Error - StatusCode: '{code}' ")]
    public static partial void LogGenericHttpError(this ILogger logger,
        string className, string methodName, HttpStatusCode code, Exception ex);

    [LoggerMessage(
        EventId = 1010,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - Warning - '{id}' NotFound")]
    public static partial void LogNotFound(this ILogger logger,
        string className, string methodName, string id,
        ApiException ex);

    #endregion

    [LoggerMessage(
        EventId = 1011,
        Level = LogLevel.Information,
        Message = $"{nameof(PaymentProviderFactory)} - " +
                  $"{nameof(PaymentProviderFactory.GetProvidersAsync)} - " +
                  $"Loading provider configurations.")]
    internal static partial void LogLoadProviders(this ILogger logger);

    [LoggerMessage(
        EventId = 1012,
        Level = LogLevel.Warning,
        Message = $"{nameof(PaymentProviderFactory)} - " +
                  $"{nameof(PaymentProviderFactory.GetProvidersAsync)} - " +
                  $"No provider configurations found.")]
    internal static partial void LogNoProvidersFound(this ILogger logger);

    [LoggerMessage(
        EventId = 1013,
        Level = LogLevel.Information,
        Message = $"{nameof(PaymentProviderFactory)} - " +
                  $"{nameof(PaymentProviderFactory.GetProvidersAsync)} - " +
                  "Loaded provider configurations - Count: '{count}' - Providers enabled: '{names}'")]
    internal static partial void LogProvidersFounded(this ILogger logger,
        int count, PaymentProviderType[] names);

    [LoggerMessage(
        EventId = 1014,
        Level = LogLevel.Information,
        Message = $"{nameof(PaymentProviderFactory)} - " +
                  $"{nameof(PaymentProviderFactory.GetProvidersAsync)} - " +
                  "Start create provider - Provider: '{provider}'")]
    internal static partial void LogStartCreateProvider(this ILogger logger,
        PaymentProviderType provider);
}
