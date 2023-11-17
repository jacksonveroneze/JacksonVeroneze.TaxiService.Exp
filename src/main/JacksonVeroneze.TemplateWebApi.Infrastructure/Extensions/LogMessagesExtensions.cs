using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

public static partial class LogMessagesExtensions
{
    #region Common

    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - NotFound - Id: '{id}' - Message: '{message}'")]
    public static partial void LogNotFound(this ILogger logger,
        string className, string methodName, Guid id, string message);

    #endregion
}
