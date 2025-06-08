using TaxiService.Common.BaseDomain.ValueObjects;

namespace TaxiService.Modules.Ride.Domain.Services;

public interface IDistanceCalculatorService
{
    public double Calculate(IList<CoordinateValueObject> positions);
}
