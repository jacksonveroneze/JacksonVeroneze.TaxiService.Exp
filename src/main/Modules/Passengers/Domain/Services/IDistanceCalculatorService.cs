using TaxiService.Modules.Passengers.Domain.ValueObjects;

namespace TaxiService.Modules.Passengers.Domain.Services;

public interface IDistanceCalculatorService
{
    public double Calculate(IList<CoordinateValueObject> positions);
}