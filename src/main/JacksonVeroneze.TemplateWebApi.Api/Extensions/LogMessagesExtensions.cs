namespace JacksonVeroneze.TemplateWebApi.Api.Extensions;

public static partial class LogMessagesExtensions
{
    #region Common

    #endregion

    #region City

    #endregion

    #region State

    [LoggerMessage(
        EventId = 2000,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info")]
    public static partial void LogGetAllStates(this ILogger logger,
        string className, string methodName);

    [LoggerMessage(
        EventId = 2001,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info")]
    public static partial void LogGetStateById(this ILogger logger,
        string className, string methodName);

    [LoggerMessage(
        EventId = 2002,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info")]
    public static partial void GetCitiesById(this ILogger logger,
        string className, string methodName);

    #endregion
}
