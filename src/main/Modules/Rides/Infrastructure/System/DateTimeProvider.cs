using System.Runtime.InteropServices;
using TaxiService.Modules.Rides.Application.Interfaces.System;

namespace TaxiService.Modules.Rides.Infrastructure.System;

[ExcludeFromCodeCoverage]
internal sealed class DateTimeProvider : IDateTimeProvider
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
        return TimeZoneInfo
            .FindSystemTimeZoneById(GetTimeZoneId());
    }

    private static string GetTimeZoneId()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return WindowsBrTimeZone;
        }

        return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
               || RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
            ? LinuxBrTimeZone
            : throw new InvalidOperationException("Invalid plataform");
    }
}