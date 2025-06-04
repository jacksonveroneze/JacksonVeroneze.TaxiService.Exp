using System.Reflection;

namespace TaxiService.Common.BaseApplication;

public static class AssemblyReference
{
    public static readonly Assembly Assembly =
        typeof(AssemblyReference).Assembly;
}
