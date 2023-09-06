using System.Runtime.InteropServices;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Common;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Common;

[ExcludeFromCodeCoverage]
public class SystemDateTime : IDateTime
{
    private const string WindowsBrTimeZone
        = "E. South America Standard Time";

    private const string LinuxBrTimeZone
        = "America/Sao_Paulo";

    private static readonly TimeZoneInfo TzInfo
        = GetTimeZoneInfo();

    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Now => TimeZoneInfo
        .ConvertTimeFromUtc(UtcNow, TzInfo);

    public DateOnly DateNow =>
        DateOnly.FromDateTime(Now);

    public TimeOnly TimeNow =>
        TimeOnly.FromDateTime(Now);

    private static TimeZoneInfo GetTimeZoneInfo()
    {
        string timeZoneId;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            timeZoneId = WindowsBrTimeZone;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                 || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            timeZoneId = LinuxBrTimeZone;
        }
        else
        {
            throw new InvalidOperationException("Invalid plataform");
        }

        return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    }
}
