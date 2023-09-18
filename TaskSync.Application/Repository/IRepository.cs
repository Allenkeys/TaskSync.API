using System.Linq.Expressions;
using TaskSync.Domain.Entities;

namespace TaskSync.Application.Repository;

public interface IRepository<T>
{
    IQueryable<T> GetAll(bool trackChanges);
    T Create(T entity);
    Task CreateBulk(IList<T> entities);
    void Update(T entity);
    void Delete(T entity);
    T FindSingleBy(Expression<Func<T, bool>> predicate, bool trackChanges);
    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, bool trackChanges);
}
