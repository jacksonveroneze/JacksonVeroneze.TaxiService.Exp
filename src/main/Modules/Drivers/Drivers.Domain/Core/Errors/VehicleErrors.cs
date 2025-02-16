using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static partial class VehicleErrors
    {
        public static Error DuplicatePlate =>
            new("Vehicle.DuplicatePlate",
                "Somente veículos pendentes podem ser ativados.");
        
        public static Error NotActive =>
            new("Vehicle.NotActive",
                "Somente veículos pendentes podem ser ativados.");

        public static Error NotPendingApproval =>
            new("Vehicle.NotPendingApproval",
                "Somente veículos pendentes podem ser aprovados ou rejeitados.");

        public static Error CannotUpdateRejectedVehicle =>
            new("Vehicle.CannotUpdateRejectedVehicle",
                "Não é possível atualizar um veículo rejeitado.");
    }
}