using System.Reflection;

namespace TaxiService.Modules.Passengers.Application;

public sealed class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}