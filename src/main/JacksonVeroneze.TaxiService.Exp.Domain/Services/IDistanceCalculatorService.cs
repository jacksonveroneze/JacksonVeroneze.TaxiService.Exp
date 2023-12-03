using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Services;

public interface IDistanceCalculatorService
{
    double Calculate(IList<CoordinateValueObject> positions);
}
