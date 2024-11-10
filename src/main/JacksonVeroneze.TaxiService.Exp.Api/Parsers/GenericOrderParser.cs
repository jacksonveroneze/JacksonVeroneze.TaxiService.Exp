using JacksonVeroneze.TaxiService.Exp.Application.v1.Models;

namespace JacksonVeroneze.TaxiService.Exp.Api.Parsers;

public static class GenericOrderParser
{
    public static OrderDto?[] Parse(
        ICollection<string> items)
    {
        OrderDto?[] itemsParsed = items
            .Select(ParseItem)
            .Where(item => item != null)
            .ToArray();

        return itemsParsed;
    }

    private static OrderDto? ParseItem(string item)
    {
        string[] parts = item.Split(':', 2);

        // SortDirection direction =
        //     Enum.Parse<SortDirection>(parts[1]);

        return parts.Length == 2
            ? new OrderDto(parts[0], parts[1])
            : null;
    }
}