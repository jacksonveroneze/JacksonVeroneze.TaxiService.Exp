namespace JacksonVeroneze.TaxiService.Exp.Application.Extensions;

internal static partial class LogMessagesExtensions
{
    #region CommonError

    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Error,
        Message = "{className} - {methodName} - Error - Message: '{message}'")]
    internal static partial void LogGenericError(this ILogger logger,
        string className, string methodName, string message);

    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Error,
        Message = "{className} - {methodName} - Error - Count: '{count}'")]
    internal static partial void LogGenericError(this ILogger logger,
        string className, string methodName, int count);

    [LoggerMessage(
        EventId = 1002,
        Level = LogLevel.Error,
        Message = "{className} - {methodName} - Error - " +
                  "Identifier: '{identifier}' - Message: '{message}'")]
    internal static partial void LogGenericError(this ILogger logger,
        string className, string methodName, Guid identifier, string message);

    #endregion

    #region CommonAlreadyExists

    [LoggerMessage(
        EventId = 1010,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - Exists - Message: '{message}'")]
    internal static partial void LogAlreadyExists(this ILogger logger,
        string className, string methodName, string message);

    [LoggerMessage(
        EventId = 1011,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - Exists - " +
                  "Identifier: '{identifier}' - Message: '{message}'")]
    internal static partial void LogAlreadyExists(this ILogger logger,
        string className, string methodName, Guid identifier, string message);

    [LoggerMessage(
        EventId = 1012,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - Exists - " +
                  "Identifier: '{identifier}' - Message: '{message}'")]
    internal static partial void LogAlreadyExists(this ILogger logger,
        string className, string methodName, string identifier, string message);

    #endregion

    #region CommonNotFound

    [LoggerMessage(
        EventId = 1020,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - NotFound - " +
                  "Identifier: '{identifier}' - Message: '{message}'")]
    internal static partial void LogNotFound(this ILogger logger,
        string className, string methodName, Guid identifier, string message);

    #endregion

    #region CommonProcessed

    [LoggerMessage(
        EventId = 1030,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Processed Success - " +
                  "Identifier: '{identifier}'")]
    internal static partial void LogProcessed(this ILogger logger,
        string className, string methodName, Guid identifier);

    [LoggerMessage(
        EventId = 1031,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - AlreadyProcessed - " +
                  "Identifier: '{identifier}' - Message: '{message}'")]
    internal static partial void LogAlreadyProcessed(this ILogger logger,
        string className, string methodName, Guid identifier, string message);

    #endregion

    #region CommonGetData

    [LoggerMessage(
        EventId = 1040,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - " +
                  "Page: '{page}' - PageSize: '{pageSize}' - " +
                  "TotalElements: '{totalElements}'")]
    internal static partial void LogGetPagedWithOffset(this ILogger logger,
        string className, string methodName,
        int page, int pageSize, int totalElements);

    [LoggerMessage(
        EventId = 1040,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - " +
                  "PageSize: '{pageSize}' - " +
                  "TotalElements: '{totalElements}' - " +
                  "HasMore: '{hasMore}'")]
    internal static partial void LogGetPagedWithCursor(this ILogger logger,
        string className, string methodName,
        int pageSize, int totalElements,
        bool hasMore);

    [LoggerMessage(
        EventId = 1041,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Found - " +
                  "Identifier: '{identifier}'")]
    internal static partial void LogGetById(this ILogger logger,
        string className, string methodName, Guid? identifier);

    #endregion

    #region CommonCreated

    [LoggerMessage(
        EventId = 1050,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Created - " +
                  "Identifier: '{identifier}'")]
    internal static partial void LogCreated(this ILogger logger,
        string className, string methodName, Guid identifier);

    #endregion

    #region CommonUpdated

    [LoggerMessage(
        EventId = 1060,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Updated - " +
                  "Identifier: '{identifier}'")]
    internal static partial void LogUpdated(this ILogger logger,
        string className, string methodName, Guid identifier);

    #endregion

    #region CommonDeleted

    [LoggerMessage(
        EventId = 1070,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Deleted - " +
                  "Identifier: '{identifier}'")]
    internal static partial void LogDeleted(this ILogger logger,
        string className, string methodName, Guid identifier);

    #endregion

    # region CommonValidationBehavior

    [LoggerMessage(
        EventId = 5000,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - Not contain validators")]
    internal static partial void LogNoContainValidators(this ILogger logger,
        string className, string methodName);

    [LoggerMessage(
        EventId = 5001,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - Total violations: '{count}'")]
    internal static partial void LogTotalViolations(this ILogger logger,
        string className, string methodName, int count);

    #endregion
}