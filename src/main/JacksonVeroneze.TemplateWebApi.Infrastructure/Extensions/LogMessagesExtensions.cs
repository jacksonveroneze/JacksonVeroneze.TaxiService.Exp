using System.Net;
using Microsoft.Extensions.Logging;
using Refit;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

public static partial class LogMessagesExtensions
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

    #region State

    [LoggerMessage(
        EventId = 3000,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - Count: '{count}'")]
    public static partial void LogGetAllStates(this ILogger logger,
        string className, string methodName, int count);

    [LoggerMessage(
        EventId = 3001,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - Id: '{id}'")]
    public static partial void LogGetStateById(this ILogger logger,
        string className, string methodName, string id);

    #endregion
}
