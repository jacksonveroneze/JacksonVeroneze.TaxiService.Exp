using JacksonVeroneze.TaxiService.Exp.ArchitectureTests.Abstractions;
using NetArchTest.Rules;
using TaxiService.Modules.Passengers.Api.Endpoints.v1.Passangers;
using TaxiService.Modules.Passengers.Application;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Infrastructure.Repositories.Passanger;
using TaxiService.Modules.Passengers.PublicApi.Services;

namespace JacksonVeroneze.TaxiService.Exp.ArchitectureTests.Layers;

public class ModuleTests : BaseTest
{
    private readonly string _domainAssembly = $"{PassangersNamespace}.Passangers.Domain";
    private readonly string _applicationAssembly = $"{PassangersNamespace}.Application";
    private readonly string _infrastructureAssembly = $"{PassangersNamespace}.Infrastructure";
    private readonly string _apiAssembly = $"{PassangersNamespace}.Api";
    private readonly string _publicApiAssembly = $"{PassangersNamespace}.PublicApi";

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherLayers()
    {
        string[] otherProjects =
        [
            _applicationAssembly,
            _infrastructureAssembly,
            _apiAssembly,
            _publicApiAssembly
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
            _infrastructureAssembly,
            _apiAssembly,
            _publicApiAssembly
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
            _applicationAssembly,
            _domainAssembly,
            _apiAssembly,
            _publicApiAssembly
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
            _domainAssembly,
            _infrastructureAssembly,
            _publicApiAssembly
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
            _infrastructureAssembly,
            _apiAssembly
        ];

        Types.InAssembly(typeof(PassangerService).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult()
            .ShouldBeSuccessful();
    }
}