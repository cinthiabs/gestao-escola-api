namespace Domain.Interfaces.Base;

public interface IUnitOfWork
{
    void BeginTransaction();
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
}