using System.Data;
using AutoMapper;
using Dapper;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Models;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Dapper;

[ExcludeFromCodeCoverage]
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

    public async Task<bool> ExistsUserAsync(string document,
        CancellationToken cancellationToken = default)
    {
        int result = await _repository
            .RecordCountAsync<UserModel>(new { name = document });

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
        int count = await _repository
            .RecordCountAsync<UserModel>();

        IEnumerable<UserModel>? result = await _repository
            .GetListPagedAsync<UserModel>(
                filter.Pagination!.Page,
                filter.Pagination!.PageSize, "", "");

        IList<UserEntity>? entities =
            _mapper.Map<IList<UserEntity>>(result);

        return entities.ToPage(filter.Pagination!, count);
    }
}