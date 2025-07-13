namespace TaxiService.Common.SharedKernel.Utils;

public static class GuidGenerator
{
    public static Guid Generate()
    {
        return Guid.CreateVersion7();
    }
}
