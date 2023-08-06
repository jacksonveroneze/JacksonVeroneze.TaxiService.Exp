namespace JacksonVeroneze.TemplateWebApi.Api.Extensions;

public static partial class LogMessagesExtensions
{
    #region Common

    [LoggerMessage(
        EventId = 2000,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info")]
    public static partial void LogGetPaged(this ILogger logger,
        string className, string methodName);

    [LoggerMessage(
        EventId = 2001,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Id: {id} - Info")]
    public static partial void LogGetById(this ILogger logger,
        string className, string methodName, Guid? id);

    #endregion
}
