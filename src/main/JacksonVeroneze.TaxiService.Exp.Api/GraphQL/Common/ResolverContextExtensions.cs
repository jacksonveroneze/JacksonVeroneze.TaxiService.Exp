using HotChocolate.Resolvers;
using JacksonVeroneze.NET.Result;
using Error = JacksonVeroneze.NET.Result.Error;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Common;

internal static class ResolverContextExtensions
{
    public static IResolverContext AddErrosFromResult(
        this IResolverContext context,
        Result result)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(result);

        IEnumerable<Error> erros = result.Errors
                                   ?? [result.Error!];

        foreach (Error item in erros)
        {
            IError error = ErrorBuilder.New()
                .SetCode(item.Code)
                .SetMessage(item.Message)
                .Build();

            context.ReportError(error);
        }

        return context;
    }
}