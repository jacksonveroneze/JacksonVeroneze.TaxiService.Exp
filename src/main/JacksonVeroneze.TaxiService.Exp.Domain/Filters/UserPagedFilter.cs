using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Filters;

[ExcludeFromCodeCoverage]
public record UserPagedFilter
{
    public string? Name { get; init; }

    public UserStatus? Status { get; init; }

    public PaginationParameters? Pagination { get; init; }

    public StringFilter? NameFilter { get; set; }
    public EnumFilter<UserStatus>? StatusFilter { get; set; }
}

public class GuidFilter
{
    public GuidFilter(Guid? value, GuidOperator op)
    {
        Value = value;
        Op = op;
    }

    public Guid? Value { get; set; }

    public GuidOperator Op { get; set; }
}

public class StringFilter
{
    public StringFilter(string? value, StringOperator op)
    {
        Value = value;
        Op = op;
    }

    public string? Value { get; set; }

    public StringOperator Op { get; set; }
}

public class NumericFilter
{
    public int? Value { get; set; }
    public NumericOperator Operator { get; set; } = NumericOperator.Equals;
}

public class EnumFilter<T> where T : struct, Enum
{
    public EnumFilter(T? value, EnumOperator? op)
    {
        Value = value;
        Operator = op;
    }

    public T? Value { get; set; }
    public EnumOperator? Operator { get; set; }
}

public enum StringOperator
{
    StartsWith,
    EndsWith,
    Contains,
    Equals
}

public enum NumericOperator
{
    GreaterThan,
    LessThan,
    Equals
}

public enum EnumOperator
{
    Contains,
    Equals,
    NotEquals
}
public enum GuidOperator
{
    Equals,
    NotEquals
}