using Microsoft.EntityFrameworkCore;

namespace TaskSync.Application.Repository;

public class UnitOfWork<TContext> : IUnitOfWork<DbContext> where TContext : DbContext
{
    private readonly TContext _context;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories == null) _repositories = new Dictionary<Type, object>();

        var type = typeof(TEntity);
        if(!_repositories.ContainsKey(type))
            _repositories[type] = new Repository<TEntity>(_context);

        return (IRepository<TEntity>)_repositories[type];
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
