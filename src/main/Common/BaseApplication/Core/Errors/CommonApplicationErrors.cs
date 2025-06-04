using System.Diagnostics.CodeAnalysis;

using JacksonVeroneze.NET.Result;

namespace TaxiService.Common.BaseApplication.Core.Errors;

[ExcludeFromCodeCoverage]
public static class CommonApplicationErrors
{
    public static class DistributedLockErrors
    {
        public static Error NotAcquired =>
            Error.Create("DistributedLock.NotAcquired", "DistributedLock not aquired");
    }
}
