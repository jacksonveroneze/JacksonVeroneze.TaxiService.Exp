using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class TransactionError
    {
        public static Error NotFound =>
            new("Transaction.NotFound",
                string.Format(null, TemplateNotFound, "transaction id"));

        public static Error StatusAlreadyPaid =>
            new("Transaction.StatusAlreadyPaid", "A transação já foi paga");

        public static Error InvalidStatusSetPaid =>
            new("Transaction.InvalidStatusSetPaid", "A transação não está aguardando pagamento");
    }
}