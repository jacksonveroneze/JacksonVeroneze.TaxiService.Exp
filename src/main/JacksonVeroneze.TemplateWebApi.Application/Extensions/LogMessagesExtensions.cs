namespace JacksonVeroneze.TemplateWebApi.Application.Extensions;

public static partial class LogMessagesExtensions
{
    #region Common

    [LoggerMessage(
        EventId = 998,
        Level = LogLevel.Error,
        Message = "{className} - {methodName} - Error - Message: '{message}'")]
    public static partial void LogGenericError(this ILogger logger,
        string className, string methodName, string message);

    [LoggerMessage(
        EventId = 999,
        Level = LogLevel.Error,
        Message = "{className} - {methodName} - Error - Message: '{message}' - Value: '{value}'")]
    public static partial void LogGenericError(this ILogger logger,
        string className, string methodName, string message, string value);

    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - Exists - Message: '{message}' - Value: '{value}'")]
    public static partial void AlreadyExists(this ILogger logger,
        string className, string methodName, string message, string value);

    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - NotFound - Id: '{id}' - Message: '{message}'")]
    public static partial void LogNotFound(this ILogger logger,
        string className, string methodName, Guid id, string message);

    [LoggerMessage(
        EventId = 1002,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Processed - Id: '{id}'")]
    public static partial void LogProcessed(this ILogger logger,
        string className, string methodName, Guid id);

    [LoggerMessage(
        EventId = 1003,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - AlreadyProcessed - Message: '{message}' - Id: '{id}'")]
    public static partial void AlreadyProcessed(this ILogger logger,
        string className, string methodName, string message, Guid id);

    [LoggerMessage(
        EventId = 1004,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - " +
                  "Page: '{page}' - PageSize: '{pageSize}' - " +
                  "TotalElements: '{totalElements}'")]
    public static partial void LogGetPaged(this ILogger logger,
        string className, string methodName,
        int page, int pageSize, int totalElements);

    [LoggerMessage(
        EventId = 1005,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Found - Id: '{id}'")]
    public static partial void LogGetById(this ILogger logger,
        string className, string methodName, Guid? id);

    [LoggerMessage(
        EventId = 1006,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Created - Id: '{id}'")]
    public static partial void LogCreated(this ILogger logger,
        string className, string methodName, Guid id);

    [LoggerMessage(
        EventId = 1007,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Deleted - Id: '{id}'")]
    public static partial void LogDeleted(this ILogger logger,
        string className, string methodName, Guid id);

    #endregion

    # region ValidationBehavior

    [LoggerMessage(
        EventId = 2000,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - Not contain validators")]
    public static partial void LogNoContainValidators(this ILogger logger,
        string className, string methodName);

    [LoggerMessage(
        EventId = 2001,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - Total Violations: '{count}'")]
    public static partial void LogTotalViolations(this ILogger logger,
        string className, string methodName, int count);

    #endregion
}
