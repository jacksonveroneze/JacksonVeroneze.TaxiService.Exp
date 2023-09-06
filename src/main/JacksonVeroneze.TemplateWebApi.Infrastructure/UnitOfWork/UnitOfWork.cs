using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.UnitOfWork;

[ExcludeFromCodeCoverage]
public class UnitOfWork : IUnitOfWork
{
    private readonly IMediator _bus;

    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext, IMediator bus)
    {
        _bus = bus;
        _dbContext = dbContext;
    }

    public async Task<bool> CommitAsync()
    {
        bool isSuccess = await _dbContext.SaveChangesAsync() > 0;

        if (!isSuccess)
        {
            throw new InvalidOperationException();
        }

        //await _bus.Publish(_dbContext);

        return isSuccess;
    }
}
