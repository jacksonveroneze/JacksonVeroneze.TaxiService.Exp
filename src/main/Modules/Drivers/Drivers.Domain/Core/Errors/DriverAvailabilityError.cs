using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class DriverAvailabilityError
    {
        public static Error CannotGoOnlineDuringRide =>
            new("DriverAvailability.CannotGoOnlineDuringRide",
                "O motorista não pode ficar online durante uma corrida.");

        public static Error CannotGoOfflineDuringRide =>
            new("DriverAvailability.CannotGoOfflineDuringRide",
                "O motorista não pode ficar offline durante uma corrida em andamento.");

        public static Error CannotStartRideWhileOffline =>
            new("DriverAvailability.CannotStartRideWhileOffline",
                "O motorista não pode iniciar uma corrida enquanto estiver offline.");

        public static Error DriverOffline =>
            new("Driver.Offline", "O motorista está OFFLINE e não pode realizar esta ação.");

        public static Error InvalidStatus =>
            new("Driver.InvalidStatus", "Status inválido");
    }
}
