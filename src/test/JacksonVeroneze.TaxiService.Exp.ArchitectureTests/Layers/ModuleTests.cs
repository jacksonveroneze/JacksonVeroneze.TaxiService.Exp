using JacksonVeroneze.TaxiService.Exp.ArchitectureTests.Abstractions;
using NetArchTest.Rules;
using TaxiService.Modules.Passengers.Api.Endpoints.v1.Passangers;
using TaxiService.Modules.Passengers.Application;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Infrastructure.Repositories.Passenger;
using TaxiService.Modules.Passengers.PublicApi.Services;

namespace JacksonVeroneze.TaxiService.Exp.ArchitectureTests.Layers;

public class ModuleTests
{
    private const string DomainAssembly = $"{UtilTests.PassangersNamespace}.Passangers.Domain";
    private const string ApplicationAssembly = $"{UtilTests.PassangersNamespace}.Application";
    private const string InfrastructureAssembly = $"{UtilTests.PassangersNamespace}.Infrastructure";
    private const string ApiAssembly = $"{UtilTests.PassangersNamespace}.Api";
    private const string PublicApiAssembly = $"{UtilTests.PassangersNamespace}.PublicApi";

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

        Types.InAssembly(typeof(PassangerEntity).Assembly)
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

        Types.InAssembly(typeof(PassangerReadRepository).Assembly)
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

        Types.InAssembly(typeof(PassangerEndpoints).Assembly)
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

        Types.InAssembly(typeof(PassangerService).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult()
            .ShouldBeSuccessful();
    }
}