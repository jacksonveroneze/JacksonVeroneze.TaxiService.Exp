using HotChocolate.Resolvers;
using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Common;

public static class ResultExtensions
{
    public static T? Match<T>(
        this Result result,
        Func<T?> onSuccess,
        Func<T?> onFailure,
        IResolverContext c)
    {
        if (result.IsSuccess)
        {
            return onSuccess();
        }

        c.AddErrosFromResult(result);

        return onFailure();
    }
}