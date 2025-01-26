using Microsoft.Extensions.Logging;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

internal static partial class LogMessagesExtensions
{
    #region Common

    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - NotFound - Id: '{id}' - Message: '{message}'")]
    internal static partial void LogNotFound(this ILogger logger,
        string className, string methodName, Guid id, string message);

    [LoggerMessage(
        EventId = 1004,
        Level = LogLevel.Information,
        Message = "{className} - {methodName} - Info - " +
                  "Page: '{page}' - PageSize: '{pageSize}' - " +
                  "TotalElements: '{totalElements}'")]
    internal static partial void LogGetPaged(this ILogger logger,
        string className, string methodName,
        int page, int pageSize, int totalElements);

    #endregion
}