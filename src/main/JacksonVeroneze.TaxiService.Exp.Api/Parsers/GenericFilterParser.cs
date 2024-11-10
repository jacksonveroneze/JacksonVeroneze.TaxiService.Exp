using JacksonVeroneze.TaxiService.Exp.Application.v1.Models;

namespace JacksonVeroneze.TaxiService.Exp.Api.Parsers;

public static class GenericFilterParser
{
    public static FilterDto?[] Parse(
        ICollection<string> items)
    {
        FilterDto?[] itemsParsed = items
            .Select(ParseItem)
            .Where(item => item != null)
            .ToArray();

        return itemsParsed;
    }

    private static FilterDto? ParseItem(string item)
    {
        ReadOnlySpan<char> valueSpan = item.AsSpan();

        // Localiza os índices dos ':' para separar campo, operador e valor
        int firstSeparatorIndex = valueSpan.IndexOf(':');

        if (firstSeparatorIndex <= 0 || firstSeparatorIndex == valueSpan.Length - 1)
        {
            return null;
        }

        int secondSeparatorIndex = valueSpan[(firstSeparatorIndex + 1)..].IndexOf(':') +
                                   firstSeparatorIndex + 1;

        if (secondSeparatorIndex <= firstSeparatorIndex ||
            secondSeparatorIndex == valueSpan.Length - 1)
        {
            return null;
        }

        // Extrai o campo, operador e valor usando Span, sem alocação extra
        ReadOnlySpan<char> fieldPart = valueSpan[..firstSeparatorIndex];

        ReadOnlySpan<char> operatorPart = valueSpan.Slice(firstSeparatorIndex + 1,
            secondSeparatorIndex - firstSeparatorIndex - 1);

        ReadOnlySpan<char> valuePart = valueSpan[(secondSeparatorIndex + 1)..];

        if (!Enum.TryParse(operatorPart.ToString(),
                true, out FilterOperator filterOperator))
        {
            return null;
        }

        // Adiciona o filtro ao FilterRequestDto
        return new FilterDto(
            fieldPart.ToString(),
            filterOperator,
            valuePart.ToString());
    }
}