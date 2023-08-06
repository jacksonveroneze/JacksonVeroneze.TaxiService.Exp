using JacksonVeroneze.TemplateWebApi.Domain.Results.Old.City;
using JacksonVeroneze.TemplateWebApi.Domain.Results.Old.State;
using Refit;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.HttpClients.Old;

public interface IIbgeApi
{
    [Get("/api/v1/localidades/estados?orderBy=nome")]
    Task<ICollection<StateResult>> GetStatesAsync(
        CancellationToken cancellationToken = default);

    [Get("/api/v1/localidades/estados/{uf}/municipios?orderBy=nome")]
    Task<ICollection<CityResult>> GetCitiesByStateAsync(
        [AliasAs("uf")] string stateId,
        CancellationToken cancellationToken = default);
}
