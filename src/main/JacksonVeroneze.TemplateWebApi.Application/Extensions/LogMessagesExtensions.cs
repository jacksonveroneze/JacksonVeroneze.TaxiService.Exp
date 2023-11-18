namespace JacksonVeroneze.TemplateWebApi.Application.Extensions;

public static partial class LogMessagesExtensions
{
    #region CommonError

    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Error,
        Message = "{className} - {methodName} - Error - Message: '{message}'")]
    public static partial void LogGenericError(this ILogger logger,
        string className, string methodName, string message);

    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Error,
        Message = "{className} - {methodName} - Error - Count: '{count}'")]
    public static partial void LogGenericError(this ILogger logger,
        string className, string methodName, int count);

    [LoggerMessage(
        EventId = 1002,
        Level = LogLevel.Error,
        Message = "{className} - {methodName} - Error - " +
                  "Identifier: '{identifier}' - Message: '{message}'")]
    public static partial void LogGenericError(this ILogger logger,
        string className, string methodName, Guid identifier, string message);

    #endregion

    #region CommonAlreadyExists

    [LoggerMessage(
        EventId = 1010,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - Exists - Message: '{message}'")]
    public static partial void LogAlreadyExists(this ILogger logger,
        string className, string methodName, string message);

    [LoggerMessage(
        EventId = 1011,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - Exists - " +
                  "Identifier: '{identifier}' - Message: '{message}'")]
    public static partial void LogAlreadyExists(this ILogger logger,
        string className, string methodName, Guid identifier, string message);

    [LoggerMessage(
        EventId = 1012,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - Exists - " +
                  "Identifier: '{identifier}' - Message: '{message}'")]
    public static partial void LogAlreadyExists(this ILogger logger,
        string className, string methodName, string identifier, string message);

    #endregion

    #region CommonNotFound

    [LoggerMessage(
        EventId = 1020,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - NotFound - " +
                  "Identifier: '{identifier}' - Message: '{message}'")]
    public static partial void LogNotFound(this ILogger logger,
        string className, string methodName, Guid identifier, string message);

    #endregion

    #region CommonProcessed

    [LoggerMessage(
        EventId = 1030,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Processed Success - " +
                  "Identifier: '{identifier}'")]
    public static partial void LogProcessed(this ILogger logger,
        string className, string methodName, Guid identifier);

    [LoggerMessage(
        EventId = 1031,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - AlreadyProcessed - " +
                  "Identifier: '{identifier}' - Message: '{message}'")]
    public static partial void LogAlreadyProcessed(this ILogger logger,
        string className, string methodName, Guid identifier, string message);

    #endregion

    #region CommonGetData

    [LoggerMessage(
        EventId = 1040,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - " +
                  "Page: '{page}' - PageSize: '{pageSize}' - " +
                  "TotalElements: '{totalElements}'")]
    public static partial void LogGetPaged(this ILogger logger,
        string className, string methodName,
        int page, int pageSize, int totalElements);

    [LoggerMessage(
        EventId = 1041,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - " +
                  "Identifier: '{identifier}'")]
    public static partial void LogGetById(this ILogger logger,
        string className, string methodName, Guid? identifier);

    #endregion

    #region CommonCreated

    [LoggerMessage(
        EventId = 1050,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Created - " +
                  "Identifier: '{identifier}'")]
    public static partial void LogCreated(this ILogger logger,
        string className, string methodName, Guid identifier);

    #endregion

    #region CommonUpdated

    [LoggerMessage(
        EventId = 1060,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Updated - " +
                  "Identifier: '{identifier}'")]
    public static partial void LogUpdated(this ILogger logger,
        string className, string methodName, Guid identifier);

    #endregion

    #region CommonDeleted

    [LoggerMessage(
        EventId = 1070,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Deleted - " +
                  "Identifier: '{identifier}'")]
    public static partial void LogDeleted(this ILogger logger,
        string className, string methodName, Guid identifier);

    #endregion

    # region CommonValidationBehavior

    [LoggerMessage(
        EventId = 5000,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - Not contain validators")]
    public static partial void LogNoContainValidators(this ILogger logger,
        string className, string methodName);

    [LoggerMessage(
        EventId = 5001,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - Total violations: '{count}'")]
    public static partial void LogTotalViolations(this ILogger logger,
        string className, string methodName, int count);

    #endregion
}
