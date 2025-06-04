namespace TaxiService.Common.BaseDomain;

public static class GuidGenerator
{
    public static Guid Generate()
    {
        return Guid.CreateVersion7();
    }
}
