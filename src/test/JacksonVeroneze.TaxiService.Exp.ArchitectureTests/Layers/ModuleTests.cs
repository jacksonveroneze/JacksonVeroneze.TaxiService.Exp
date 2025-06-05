using JacksonVeroneze.TaxiService.Exp.ArchitectureTests.Abstractions;
using NetArchTest.Rules;
using TaxiService.Modules.Passenger.Api.Endpoints.v1.Passengers;
using TaxiService.Modules.Passenger.Application;
using TaxiService.Modules.Passenger.Application.v1.Passenger.PublicApi;
using TaxiService.Modules.Passenger.Domain.Aggregates;
using TaxiService.Modules.Passenger.Infrastructure.Repositories.Passenger;

namespace JacksonVeroneze.TaxiService.Exp.ArchitectureTests.Layers;

public class ModuleTests
{
    private const string DomainAssembly = $"{UtilTests.PassengersNamespace}.Passenger.Domain";
    private const string ApplicationAssembly = $"{UtilTests.PassengersNamespace}.Application";
    private const string InfrastructureAssembly = $"{UtilTests.PassengersNamespace}.Infrastructure";
    private const string ApiAssembly = $"{UtilTests.PassengersNamespace}.Api";
    private const string PublicApiAssembly = $"{UtilTests.PassengersNamespace}.PublicApi";

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherLayers()
    {
        string[] otherProjects =
        [
            ApplicationAssembly,
            InfrastructureAssembly,
            ApiAssembly,
            PublicApiAssembly
        ];

        Types.InAssembly(typeof(Passenger).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherLayers()
    {
        string[] otherProjects =
        [
            InfrastructureAssembly,
            ApiAssembly,
            PublicApiAssembly
        ];

        Types.InAssembly(typeof(AssemblyReference).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherLayers()
    {
        string[] otherProjects =
        [
            ApplicationAssembly,
            DomainAssembly,
            ApiAssembly,
            PublicApiAssembly
        ];

        Types.InAssembly(typeof(PassengerReadRepository).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void Api_Should_Not_HaveDependencyOnOtherLayers()
    {
        string[] otherProjects =
        [
            DomainAssembly,
            InfrastructureAssembly,
            PublicApiAssembly
        ];

        Types.InAssembly(typeof(PassengersEndpoints).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void PublicApi_Should_Not_HaveDependencyOnOtherLayers()
    {
        string[] otherProjects =
        [
            InfrastructureAssembly,
            ApiAssembly
        ];

        Types.InAssembly(typeof(PassengerPublicApi).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult()
            .ShouldBeSuccessful();
    }
}
