using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Entities.Base;
using TaxiService.Common.BaseDomain.Enums;
using TaxiService.Common.BaseDomain.ValueObjects;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Domain.Entities;

public class VehicleEntity : BaseEntityAggregateRoot
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

    public VehicleEntity()
    {
    }

    public VehicleEntity(Guid driverId,
        string? model, string? brand,
        string? plate, string? color,
        int? year)
    {
        ArgumentNullException.ThrowIfNull(driverId);
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(brand);
        ArgumentNullException.ThrowIfNull(plate);
        ArgumentNullException.ThrowIfNull(color);
        ArgumentNullException.ThrowIfNull(year);

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
            return Result.FromInvalid(
                DomainErrors.VehicleErrors.NotPendingApproval);
        }

        Status = VehicleStatus.Active;
        MarkAsUpdated();

        return Result.WithSuccess();
    }

    public Result Inactivate()
    {
        if (Status != VehicleStatus.Active)
        {
            return Result.FromInvalid(
                DomainErrors.VehicleErrors.NotActive);
        }

        Status = VehicleStatus.Inactive;
        MarkAsUpdated();

        return Result.WithSuccess();
    }

    public Result Reject()
    {
        if (Status != VehicleStatus.PendingActivation)
        {
            return Result.FromInvalid(
                DomainErrors.VehicleErrors.NotPendingApproval);
        }

        Status = VehicleStatus.Rejected;
        MarkAsUpdated();

        return Result.WithSuccess();
    }

    #endregion

    #region Factory

    // Factory Method
    public static Result<VehicleEntity> Create(
        Guid driverId,
        string? model, string? brand,
        string? plate, string? color,
        int? year)
    {
        VehicleEntity entity = new(driverId,
            model, brand, plate, color, year);

        return Result<VehicleEntity>.WithSuccess(entity);
    }

    #endregion
}