using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TaskSync.Application.Repository;
public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _dbContext;
    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public T Create(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

    public T FindSingleBy(Expression<Func<T, bool>> predicate, bool trackChanges)
    {
        return trackChanges
           ? _dbContext.Set<T>().Where(predicate).SingleOrDefault()
           : _dbContext.Set<T>().Where(predicate).AsNoTracking().SingleOrDefault();
    }
    public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, bool trackChanges)
    {
        var result = trackChanges 
            ? _dbContext.Set<T>().Where(predicate)
            : _dbContext.Set<T>().Where(predicate).AsNoTracking();
        return result;
    }

    public IQueryable<T> GetAll(bool trackChanges)
    {
        return trackChanges ? _dbContext.Set<T>() : _dbContext.Set<T>().AsNoTracking();
    }

    public void Update(T entity) => _dbContext.Set<T>().Update(entity);

}
