using System.Reflection;

namespace TaxiService.Modules.Payment.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly =
        typeof(AssemblyReference).Assembly;
}
