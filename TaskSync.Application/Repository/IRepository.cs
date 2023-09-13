using System.Linq.Expressions;

namespace TaskSync.Application.Repository;

public interface IRepository<T>
{
    IQueryable<T> GetAll(bool trackChanges);
    T Create(T entity);
    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, bool trackChanges);
}
