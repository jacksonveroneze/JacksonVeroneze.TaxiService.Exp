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

    #endregion

    #region City

    [LoggerMessage(
        EventId = 2000,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - StateId: '{id}'")]
    public static partial void LogGetCityByState(this ILogger logger,
        string className, string methodName, string id);

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