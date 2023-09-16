using System.Linq.Expressions;

namespace TaskSync.Application.Repository;

public interface IRepository<T>
{
    IQueryable<T> GetAll(bool trackChanges);
    T Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    T FindSingleBy(Expression<Func<T, bool>> predicate, bool trackChanges);
    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, bool trackChanges);
}
