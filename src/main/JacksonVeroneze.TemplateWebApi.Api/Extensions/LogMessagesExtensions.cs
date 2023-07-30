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
    public static partial void LogGetPagedStates(this ILogger logger,
        string className, string methodName);

    [LoggerMessage(
        EventId = 2001,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Id: {id} - Info")]
    public static partial void LogGetStateById(this ILogger logger,
        string className, string methodName, string id);

    [LoggerMessage(
        EventId = 2002,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - StateId: {stateId} - Info")]
    public static partial void GetPagedCitiesByStateId(this ILogger logger,
        string className, string methodName, string stateId);

    #endregion
}
