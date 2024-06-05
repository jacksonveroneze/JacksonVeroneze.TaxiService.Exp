using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Contexts;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.UnitOfWork;

[ExcludeFromCodeCoverage]
public class EfCoreUnitOfWork(
    ApplicationDbContext dbContext)
    : IUnitOfWork
{
    public async Task<bool> CommitAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            bool isSuccess = await dbContext
                .SaveChangesAsync(cancellationToken) > 0;

            if (!isSuccess)
            {
                throw new InvalidOperationException();
            }

            return true;
        }
        catch (Exception e)
        {
            string a = e.Message;
            throw;
        }
    }
}