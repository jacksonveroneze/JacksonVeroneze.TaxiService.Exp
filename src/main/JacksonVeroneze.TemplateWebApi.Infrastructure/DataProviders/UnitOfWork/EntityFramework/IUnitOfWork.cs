namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.UnitOfWork.EntityFramework;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
