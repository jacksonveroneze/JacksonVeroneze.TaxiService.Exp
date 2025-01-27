using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.Application;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MediatorExtensions
{
    public static MediatRServiceConfiguration AddPassengersMediatrHandlers(
        this MediatRServiceConfiguration conf)
    {
        conf.RegisterServicesFromAssemblyContaining<AssemblyReference>();

        return conf;
    }
}