using Microsoft.Extensions.Logging;
using Moq;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Extensions;

public static class MockLogger
{
    public static void MockLogLevel<T>(
        this Mock<ILogger<T>> mockLogger)
    {
        ArgumentNullException.ThrowIfNull(mockLogger);

        mockLogger
            .Setup(mock => mock.IsEnabled(LogLevel.Information))
            .Returns(true);

        mockLogger
            .Setup(mock => mock.IsEnabled(LogLevel.Warning))
            .Returns(true);

        mockLogger
            .Setup(mock => mock.IsEnabled(LogLevel.Error))
            .Returns(true);
    }
}
