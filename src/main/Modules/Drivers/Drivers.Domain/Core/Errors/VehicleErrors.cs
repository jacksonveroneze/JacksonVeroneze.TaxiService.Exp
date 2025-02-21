using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Core.Errors;

namespace TaxiService.Modules.Drivers.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static partial class VehicleErrors
    {
        public static Error NotFound =>
            new("Vehicle.NotFound",
                string.Format(null, CommonDomainErrors.TemplateNotFound, "vehicle id"));

        public static Error DuplicatePlate =>
            new("Vehicle.DuplicatePlate",
                "Veículo com a placa informada já cadastrado.");

        public static Error DuplicateDriverId =>
            new("Vehicle.DuplicateDriverId",
                "O motorista informado já possui veículo cadastrado.");

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