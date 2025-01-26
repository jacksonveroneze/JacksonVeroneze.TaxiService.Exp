using HotChocolate.Resolvers;
using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Common;

internal static class ResultExtensions
{
    public static T? Match<T>(
        this Result result,
        Func<T?> onSuccess,
        Func<T?> onFailure,
        IResolverContext c)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        if (result.IsSuccess)
        {
            return onSuccess();
        }

        c.AddErrosFromResult(result);

        return onFailure();
    }
}