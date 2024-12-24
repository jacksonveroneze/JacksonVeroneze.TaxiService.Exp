using JacksonVeroneze.NET.Pagination.Common;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Queries.Base;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Domain.Util;
using JacksonVeroneze.TaxiService.Exp.Domain.Util.Operators;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;

public sealed record GetUserPagedQuery() :
    PagedCursorQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetUserPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(UserResponse.FullName);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public string? Name { get; init; }

    public UserStatus? Status { get; init; }

    // public ICollection<string>? Filters { get; init; }

    public StringFilter NameFilter
    {
        get
        {
            string[] data = Name?.Replace("[", string.Empty).Replace("]", string.Empty).Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)!;

            QueryOperators op = Enum.Parse<QueryOperators>(data![0]);

            StringOperator cond = op switch
            {
                QueryOperators.Contains => StringOperator.Contains,
                QueryOperators.StartsWith => StringOperator.StartsWith,
                QueryOperators.EndsWith => StringOperator.EndsWith,
                _ => StringOperator.Equals
            };

            return StringFilter.Create(data![1], cond);
        }
    }
}

public enum QueryOperators
{
    Equals,
    Contains,
    StartsWith,
    EndsWith
}