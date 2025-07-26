namespace TaxiService.Common.SharedKernel.Interfaces;

public interface IMetricsPublisher
{
    void Counter(string name, double increment = 1, 
        IDictionary<string, string>? tags = null);
}
