using Microsoft.Extensions.Logging;
using Moq;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Extensions;

public static class VerifyLogger
{
    public static void Verify<T>(
        this Mock<ILogger<T>> logger,
        string expectedMessage,
        LogLevel expectedLogLevel = LogLevel.Information,
        Func<Times>? times = null)
    {
        ArgumentNullException.ThrowIfNull(logger);

        times ??= Times.Once;

        Func<object, Type, bool> state = (x, __)
            => x.ToString()!.Contains(expectedMessage,
                StringComparison.OrdinalIgnoreCase);

        logger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == expectedLogLevel),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => state(v, t)),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!), times);
    }
}
