namespace JacksonVeroneze.TemplateWebApi.Application.Extensions;

public static partial class LogMessagesExtensions
{
    #region Common

    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - NotFound")]
    public static partial void LogNotFound(this ILogger logger,
        string className, string methodName);

    [LoggerMessage(
        EventId = 6000,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - Count: '{count}'")]
    public static partial void LogGetPaged(this ILogger logger,
        string className, string methodName, int count);

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
