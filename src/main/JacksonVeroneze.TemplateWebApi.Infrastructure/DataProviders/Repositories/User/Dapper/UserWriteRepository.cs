using System.Data;
using Dapper;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Models;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Dapper;

public class UserWriteRepository : IUserWriteRepository
{
    private readonly IDbConnection _repository;

    public UserWriteRepository(
        IDbConnection connection)
    {
        _repository = connection;
    }

    public Task CreateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        UserModel result = new()
        {
            // Id = entity.Id,
            // Name = entity.Name,
            // Status = (int)entity.Status
        };

        return _repository.InsertAsync<Guid, UserModel>(result);
    }

    public Task DeleteAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        return _repository.DeleteAsync<UserModel>(entity.Id);
    }

    public Task UpdateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        UserModel result = new()
        {
            // Id = entity.Id,
            // Name = entity.Name,
            // Status = (int)entity.Status
        };

        return _repository.UpdateAsync<UserModel>(result);
    }
}
