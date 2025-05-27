using Microsoft.Extensions.Logging;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

public static partial class LogMessagesExtensions
{
    [LoggerMessage(
        EventId = 1041,
        Level = LogLevel.Information,
        Message = "{contextName} - {className} - {methodName} - " +
                  "Identifier: '{identifier}' - IsFound: {found}")]
    public static partial void LogGetById(this ILogger logger,
        string contextName, string className, string methodName, Guid? identifier, bool found);

}
