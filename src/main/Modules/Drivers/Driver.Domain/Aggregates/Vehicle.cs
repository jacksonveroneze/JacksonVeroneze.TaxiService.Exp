using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Modules.Driver.Domain.Core.Errors;
using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.Domain.Aggregates;

public class Vehicle : BaseAggregateRoot
{
    public Guid Id { get; private set; }

    public Guid DriverId { get; private set; }

    public string? Model { get; private set; }

    public string? Brand { get; private set; }

    public string? Plate { get; private set; }

    public string? Color { get; private set; }

    public int? Year { get; private set; }

    public VehicleStatus? Status { get; private set; }

    #region ctor

    public Vehicle()
    {
    }

    public Vehicle(Guid driverId,
        string? model, string? brand,
        string? plate, string? color,
        int? year)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(brand);
        ArgumentNullException.ThrowIfNull(plate);
        ArgumentNullException.ThrowIfNull(color);

        Id = GuidGenerator.Generate();
        DriverId = driverId;
        Model = model;
        Brand = brand;
        Plate = plate;
        Color = color;
        Year = year;

        Status = VehicleStatus.PendingActivation;
    }

    #endregion

    #region Status

    public Result Activate()
    {
        if (Status != VehicleStatus.PendingActivation)
        {
            return Result.FromRuleViolation(
                DomainErrors.VehicleErrors.NotPendingApproval);
        }

        Status = VehicleStatus.Active;

        return Result.WithSuccess();
    }

    public Result Inactivate()
    {
        if (Status != VehicleStatus.Active)
        {
            return Result.FromRuleViolation(
                DomainErrors.VehicleErrors.NotActive);
        }

        Status = VehicleStatus.Inactive;

        return Result.WithSuccess();
    }

    public Result Reject()
    {
        if (Status != VehicleStatus.PendingActivation)
        {
            return Result.FromRuleViolation(
                DomainErrors.VehicleErrors.NotPendingApproval);
        }

        Status = VehicleStatus.Rejected;

        return Result.WithSuccess();
    }

    #endregion

    #region Factory

    public static Result<Vehicle> Create(
        Guid driverId,
        string? model, string? brand,
        string? plate, string? color,
        int? year)
    {
        Vehicle entity = new(driverId,
            model, brand, plate, color, year);

        return Result<Vehicle>.WithSuccess(entity);
    }

    #endregion
}
