namespace JacksonVeroneze.TemplateWebApi.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
