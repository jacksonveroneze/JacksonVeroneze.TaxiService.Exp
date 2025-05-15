using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Core.Errors;

namespace TaxiService.Modules.Rides.Domain.Core.Errors;

public static class DomainErrors
{
    public static class RideError
    {
        public static Error NotFound =>
            Error.Create("Ride.NotFound",
                string.Format(null, CommonDomainErrors.TemplateNotFound, "ride id"));

        public static Error StatusAlreadyDefined =>
            Error.Create("Ride.StatusAlreadyDefined", "Corrida já está no status informado");

        public static Error InvalidStatusSetAccept =>
            Error.Create("Ride.InvalidStatusSetAccept", "Corrida não está no status requested");

        public static Error InvalidStatusSetStart =>
            Error.Create("Ride.InvalidStatusSetStart", "Corrida não está no status accepted");

        public static Error InvalidStatusSetFinish =>
            Error.Create("Ride.InvalidStatusSetFinish", "Corrida não está no status in progress");

        public static Error InvalidStatusSetCancel =>
            Error.Create("Ride.InvalidStatusSetCancel", "Corrida já foi finalizada ou cancelada");

        public static Error DriverAlready =>
            Error.Create("Ride.DriverAlready", "Motorista já informado");

        public static Error AlreadyByPassanger =>
            Error.Create("Ride.AlreadyByPassanger", "Passageiro já tem corrida ativa");

        public static Error InvalidStatusAddPosition =>
            Error.Create("Ride.InvalidStatusAddPosition",
                "Para adicionar posições a corrida deve estar em progresso.");
    }
}
