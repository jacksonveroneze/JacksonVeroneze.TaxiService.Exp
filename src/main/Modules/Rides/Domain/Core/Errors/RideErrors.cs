using JacksonVeroneze.NET.Result;
using TaxiService.Common.Domain.Core.Errors;

namespace TaxiService.Modules.Rides.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class RideError
    {
        public static Error NotFound =>
            new("Ride.NotFound",
                string.Format(null, CommonDomainErrors.TemplateNotFound, "ride id"));

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
}