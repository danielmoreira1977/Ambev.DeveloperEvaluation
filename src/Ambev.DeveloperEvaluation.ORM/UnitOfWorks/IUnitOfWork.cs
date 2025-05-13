namespace Ambev.DeveloperEvaluation.ORM.UnitOfWorks;

public interface IUnitOfWork
{
    DefaultContext DefaultContext { get; }

    Task BeginTransactionAsync();

    Task<int> CommitAsync();

    Task RollbackAsync();
}
