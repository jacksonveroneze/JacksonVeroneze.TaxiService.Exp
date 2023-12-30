using System.Diagnostics.CodeAnalysis;
using System.Text;
using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;

[ExcludeFromCodeCoverage]
public static class DomainErrors
{
    private static readonly CompositeFormat TemplateNotFound =
        CompositeFormat.Parse("The {0} with the specified identifier was not found.");

    private static readonly CompositeFormat TemplateDataInUse =
        CompositeFormat.Parse("The specified {0} is already in use.");

    private static readonly CompositeFormat TemplateDataInvalid =
        CompositeFormat.Parse("The specified {0} is invalid.");

    public static class CommonError
    {
        public static Error NotFound =>
            new("Commmon.NotFound",
                string.Format(null, TemplateNotFound, "resource"));
    }

    public static class UserError
    {
        public static Error NotFound =>
            new("User.NotFound",
                string.Format(null, TemplateNotFound, "user id"));

        public static Error InvalidName =>
            new("User.InvalidName",
                string.Format(null, TemplateDataInvalid, "name"));

        public static Error InvalidCpf =>
            new("User.InvalidCpf",
                string.Format(null, TemplateDataInvalid, "cpf"));

        public static Error DuplicateCpf =>
            new("User.DuplicateCpf",
                string.Format(null, TemplateDataInUse, "cpf"));

        public static Error AlreadyActivated =>
            new("User.AlreadyActivated",
                "The user has already been activated.");

        public static Error AlreadyInactivated =>
            new("User.AlreadyInactivated",
                "The user has already been inactivated.");
    }

    public static class EmailError
    {
        public static Error NotFound =>
            new("Email.NotFound",
                string.Format(null, TemplateNotFound, "e-mail"));

        public static Error DuplicateEmail =>
            new("Email.DuplicateEmail",
                string.Format(null, TemplateDataInUse, "e-mail"));

        public static Error InvalidEmail =>
            new("Email.InvalidEmail",
                string.Format(null, TemplateDataInvalid, "e-mail"));
    }

    public static class CoordinateError
    {
        public static Error InvalidCoordinate =>
            new("Coordinate.InvalidCoordinate",
                string.Format(null, TemplateDataInvalid, "coordinate"));
    }

    public static class RideError
    {
        public static Error NotFound =>
            new("Ride.NotFound",
                string.Format(null, TemplateNotFound, "ride id"));

        public static Error StatusAlreadyDefined =>
            new("Ride.StatusAlreadyDefined", "Corrida já está no status informado");

        public static Error InvalidStatusSetAccept =>
            new("Ride.InvalidStatusSetAccept", "Corrida não está no status requested");

        public static Error InvalidStatusSetStart =>
            new("Ride.InvalidStatusSetStart", "Corrida não está no status accepted");

        public static Error InvalidStatusSetFinish =>
            new("Ride.InvalidStatusSetFinish", "Corrida não está no status in progress");

        public static Error InvalidStatusSetCancel =>
            new("Ride.InvalidStatusSetCancel", "Corrida já foi finalizada ou cancelada");

        public static Error DriverAlready =>
            new("Ride.DriverAlready", "Motorista já informado");

        public static Error AlreadyByUser =>
            new("Ride.AlreadyByUser", "Usuário já tem corrida ativa");

        public static Error InvalidStatusAddPosition =>
            new("Ride.InvalidStatusAddPosition",
                "Para adicionar posições a corrida deve estar em progresso.");
    }

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

    public static class MoneyError
    {
        public static Error InvalidValue =>
            new("Money.InvalidValue", "Valor inválido");
    }
}