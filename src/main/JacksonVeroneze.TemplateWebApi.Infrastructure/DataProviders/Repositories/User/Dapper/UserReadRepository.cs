using System.Data;
using AutoMapper;
using Dapper;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Models;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Dapper;

public class UserReadRepository : IUserReadRepository
{
    private readonly IMapper _mapper;
    private readonly IDbConnection _repository;

    public UserReadRepository(
        IMapper mapper,
        IDbConnection connection)
    {
        _mapper = mapper;
        _repository = connection;
    }

    public async Task<bool> ExistsByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        int result = await _repository
            .RecordCountAsync<UserModel>(new { name = name });

        return result > 0;
    }

    public async Task<UserEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        UserModel? result = await _repository
            .GetAsync<UserModel>(id);

        if (result is null)
        {
            return null;
        }

        return _mapper.Map<UserEntity>(result);
    }

    public async Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        IEnumerable<UserModel>? result = await _repository
            .GetListAsync<UserModel>();

        IList<UserEntity>? entities =
            _mapper.Map<IList<UserEntity>>(result);

        return entities.ToPageInMemory(filter.Pagination!);
    }
}
