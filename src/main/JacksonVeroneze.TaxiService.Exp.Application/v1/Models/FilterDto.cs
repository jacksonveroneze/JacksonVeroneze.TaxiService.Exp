namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models;

public record FilterDto(string? Field, FilterOperator? Operator, string? Value);

public enum FilterOperator
{
    Eq, // Equal (==)
    Ne, // Not Equal (!=)
    Gt, // Greater Than (>)
    Gte, // Greater Than or Equal (>=)
    Lt, // Less Than (<)
    Lte, // Less Than or Equal (<=)
    Contains, // Contains (para strings, ex.: LIKE '%value%')
    StartsWith, // Starts With (ex.: LIKE 'value%')
    EndsWith // Ends With (ex.: LIKE '%value')
}

public record OrderDto(string? Field, string? Direction);
