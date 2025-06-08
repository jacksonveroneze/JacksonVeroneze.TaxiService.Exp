using System.Reflection;

namespace TaxiService.Modules.Ride.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly =
        typeof(AssemblyReference).Assembly;
}
