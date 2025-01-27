using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Rides.Application;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MediatorExtensions
{
    public static MediatRServiceConfiguration AddRidesMediatrHandlers(
        this MediatRServiceConfiguration conf)
    {
        conf.RegisterServicesFromAssemblyContaining<AssemblyReference>();

        return conf;
    }
}