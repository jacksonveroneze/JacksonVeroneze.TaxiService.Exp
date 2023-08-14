namespace JacksonVeroneze.TemplateWebApi.Application.Extensions;

public static partial class LogMessagesExtensions
{
    #region Common

    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - NotFound - Id: '{id}'")]
    public static partial void LogNotFound(this ILogger logger,
        string className, string methodName, Guid? id);

    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - NotFound - Id: '{id}'")]
    public static partial void AlreadyProcessed(this ILogger logger,
        string className, string methodName, Guid? id);

    [LoggerMessage(
        EventId = 6000,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - " +
                  "Page: '{page}' - PageSize: '{pageSize}' - " +
                  "TotalElements: '{totalElements}'")]
    public static partial void LogGetPaged(this ILogger logger,
        string className, string methodName,
        int page, int pageSize, int totalElements);

    [LoggerMessage(
        EventId = 6001,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - Id: '{id}'")]
    public static partial void LogGetById(this ILogger logger,
        string className, string methodName, Guid? id);

    [LoggerMessage(
        EventId = 6001,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - Created")]
    public static partial void LogCreated(this ILogger logger,
        string className, string methodName);



    [LoggerMessage(
        EventId = 6001,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Deleted - Id: '{id}'")]
    public static partial void LogDeleted(this ILogger logger,
        string className, string methodName, Guid id);

    #endregion

    [LoggerMessage(
        EventId = 6001,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Activated - Id: '{id}'")]
    public static partial void LogActivated(this ILogger logger,
        string className, string methodName, Guid id);

    [LoggerMessage(
        EventId = 6002,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Inactivated - Id: '{id}'")]
    public static partial void LogInactivated(this ILogger logger,
        string className, string methodName, Guid id);

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
