using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class DriverAvailabilityError
    {
        public static Error CannotGoOnlineDuringRide =>
            Error.Create("DriverAvailability.CannotGoOnlineDuringRide",
                "O motorista não pode ficar online durante uma corrida.");

        public static Error CannotGoOfflineDuringRide =>
            Error.Create("DriverAvailability.CannotGoOfflineDuringRide",
                "O motorista não pode ficar offline durante uma corrida em andamento.");

        public static Error CannotStartRideWhileOffline =>
            Error.Create("DriverAvailability.CannotStartRideWhileOffline",
                "O motorista não pode iniciar uma corrida enquanto estiver offline.");

        public static Error DriverOffline =>
            Error.Create("Driver.Offline", "O motorista está OFFLINE e não pode realizar esta ação.");

        public static Error InvalidStatus =>
            Error.Create("Driver.InvalidStatus", "Status inválido");
    }
}
