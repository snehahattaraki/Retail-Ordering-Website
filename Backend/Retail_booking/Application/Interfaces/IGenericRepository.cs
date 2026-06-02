using System.Linq.Expressions;
using RetailOrdering.Domain.Common;

namespace RetailOrdering.Application.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
}
