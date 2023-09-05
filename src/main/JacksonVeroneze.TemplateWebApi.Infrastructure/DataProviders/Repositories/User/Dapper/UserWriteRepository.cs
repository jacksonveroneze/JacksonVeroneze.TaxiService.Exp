using System.Data;
using AutoMapper;
using Dapper;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Models;
using MediatR;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Dapper;

public class UserWriteRepository : IUserWriteRepository
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IDbConnection _repository;

    public UserWriteRepository(
        IMapper mapper,
        IMediator mediator,
        IDbConnection connection)
    {
        _mapper = mapper;
        _mediator = mediator;
        _repository = connection;
    }

    public async Task CreateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        UserModel model = _mapper.Map<UserModel>(entity);

        await _repository.InsertAsync<Guid, UserModel>(model);

        await DispatchEventsAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync<UserModel>(entity.Id);

        await DispatchEventsAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        UserModel model = _mapper.Map<UserModel>(entity);

        await _repository.UpdateAsync<UserModel>(model);

        await DispatchEventsAsync(entity, cancellationToken);
    }

    private async Task DispatchEventsAsync(
        UserEntity entity,
        CancellationToken cancellationToken)
    {
        IEnumerable<Task>? tasks = entity.Events?
            .Select(async evt => await _mediator
                .Publish(evt, cancellationToken));

        await Task.WhenAll(tasks!);
    }
}
