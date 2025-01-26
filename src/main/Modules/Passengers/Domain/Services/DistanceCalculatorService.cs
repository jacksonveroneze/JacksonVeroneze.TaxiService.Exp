using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Services;

public class DistanceCalculatorService : IDistanceCalculatorService
{
    public double Calculate(IList<CoordinateValueObject> positions)
    {
        double distancia = 0;

        for (int i = 0; i < positions.Count - 1; i++)
        {
            CoordinateValueObject coordenadaAtual = positions[i];
            CoordinateValueObject proximaCoordenada = positions[i + 1];

            distancia += Calculate(coordenadaAtual, proximaCoordenada);
        }

        return distancia;
    }

    private static double Calculate(
        CoordinateValueObject coord1, CoordinateValueObject coord2)
    {
        const double raioTerra = 6371;

        const double radiansOverDegrees = Math.PI / 180.0;

        double lat1 = coord1.Latitude * radiansOverDegrees;
        double lon1 = coord1.Longitude * radiansOverDegrees;
        double lat2 = coord2.Latitude * radiansOverDegrees;
        double lon2 = coord2.Longitude * radiansOverDegrees;

        double dLat = lat2 - lat1;
        double dLon = lon2 - lon1;

        double a = (Math.Sin(dLat / 2) * Math.Sin(dLat / 2)) +
                   (Math.Cos(lat1) * Math.Cos(lat2) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2));

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return raioTerra * c;
    }
}