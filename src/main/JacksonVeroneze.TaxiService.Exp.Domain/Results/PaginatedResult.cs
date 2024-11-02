namespace JacksonVeroneze.TaxiService.Exp.Domain.Results;

public record PaginatedResult<T>(
    ICollection<T> Data,
    bool HasMore,
    string Cursor);