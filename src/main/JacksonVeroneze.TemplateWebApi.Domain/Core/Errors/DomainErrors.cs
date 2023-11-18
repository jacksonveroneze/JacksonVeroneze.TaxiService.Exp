using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

[ExcludeFromCodeCoverage]
public static class DomainErrors
{
    private const string TemplateNotFound = "The {0} with the specified identifier was not found.";
    private const string TemplateDataInUse = "The specified {0} is already in use.";
    private const string TemplateDataInvalid = "The specified {0} is invalid.";

    public static class User
    {
        public static Error NotFound =>
            new("User.NotFound",
                string.Format(TemplateNotFound, "user id"));

        public static Error InvalidName =>
            new("User.InvalidName",
                string.Format(TemplateDataInvalid, "name"));

        public static Error InvalidCpf =>
            new("User.InvalidCpf",
                string.Format(TemplateDataInvalid, "cpf"));

        public static Error DuplicateCpf =>
            new("User.DuplicateCpf",
                string.Format(TemplateDataInUse, "cpf"));

        public static Error AlreadyActivated =>
            new("User.AlreadyActivated",
                "The user has already been activated.");

        public static Error AlreadyInactivated =>
            new("User.AlreadyInactivated",
                "The user has already been inactivated.");
    }

    public static class Email
    {
        public static Error NotFound =>
            new("Email.NotFound",
                string.Format(TemplateNotFound, "e-mail"));

        public static Error DuplicateEmail =>
            new("Email.DuplicateEmail",
                string.Format(TemplateDataInUse, "e-mail"));

        public static Error InvalidEmail =>
            new("Email.InvalidEmail",
                string.Format(TemplateDataInvalid, "e-mail"));
    }

    public static class Phone
    {
        public static Error NotFound =>
            new("Phone.NotFound",
                string.Format(TemplateNotFound, "phone"));

        public static Error DuplicatePhone =>
            new("Phone.DuplicatePhone",
                string.Format(TemplateDataInUse, "phone"));

        public static Error InvalidPhone =>
            new("Phone.InvalidPhone",
                string.Format(TemplateDataInvalid, "phone"));
    }

    public static class Coordinate
    {
        public static Error InvalidCoordinate =>
            new("Coordinate.InvalidCoordinate",
                string.Format(TemplateDataInvalid, "coordinate"));
    }

    public static class Ride
    {
        public static Error NotFound =>
            new("Ride.NotFound",
                string.Format(TemplateNotFound, "ride id"));

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
            new("Ride.AlreadyByUser", "Usuário já tem corrida no status: Pending ou Inprogress");
    }
}
