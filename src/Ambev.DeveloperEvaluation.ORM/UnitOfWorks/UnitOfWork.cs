using Microsoft.EntityFrameworkCore.Storage;

namespace Ambev.DeveloperEvaluation.ORM.UnitOfWorks;

public class UnitOfWork(DefaultContext defaultContext) : IDisposable, IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    public DefaultContext DefaultContext => defaultContext;

    public async Task BeginTransactionAsync() => _transaction ??= await DefaultContext.Database.BeginTransactionAsync();

    public async Task<int> CommitAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        return await DefaultContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        DefaultContext.Dispose();
        GC.SuppressFinalize(this); // Added to address CA1816
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}
