namespace TaxiService.Common.SharedKernel.Utils;

public static class TagExtensions
{
    public static IDictionary<string, string> ToDictionary(this object tags)
    {
        return tags.GetType()
            .GetProperties()
            .ToDictionary(p => p.Name, p => p.GetValue(tags)?.ToString() ?? string.Empty,
                StringComparer.OrdinalIgnoreCase);
    }
}
