using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Core.Errors;

namespace TaxiService.Modules.Drivers.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class VehicleErrors
    {
        public static Error NotFound =>
            Error.Create("Vehicle.NotFound",
                string.Format(null, CommonDomainErrors.TemplateNotFound, "vehicle id"));

        public static Error DuplicatePlate =>
            Error.Create("Vehicle.DuplicatePlate",
                "Veículo com a placa informada já cadastrado.");

        public static Error DuplicateDriverId =>
            Error.Create("Vehicle.DuplicateDriverId",
                "O motorista informado já possui veículo cadastrado.");

        public static Error NotActive =>
            Error.Create("Vehicle.NotActive",
                "Somente veículos pendentes podem ser ativados.");

        public static Error NotPendingApproval =>
            Error.Create("Vehicle.NotPendingApproval",
                "Somente veículos pendentes podem ser aprovados ou rejeitados.");

        public static Error CannotUpdateRejectedVehicle =>
            Error.Create("Vehicle.CannotUpdateRejectedVehicle",
                "Não é possível atualizar um veículo rejeitado.");
    }
}
