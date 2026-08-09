

using System.Linq.Expressions;

namespace Mapper.Domain.Interfaces
{
    public interface IRepository<T,K>
    {
        Task<bool> ExistByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<bool> ContainsByEntityAsync(T entity, CancellationToken ct = default);
        Task<int> CountByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task CreateAsync(T entity, CancellationToken ct = default);
        Task CreateListAsync(List<T> entities, CancellationToken ct = default);
        Task<T?> GetByIdAsync(K id, CancellationToken ct = default);
        Task<T?> GetByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<List<T>?> GetAllAsync(CancellationToken ct = default);
        Task<List<T>?> GetListByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task UpdateAsync(T entity, CancellationToken ct = default);
        Task UpdateListAsync(List<T> entities, CancellationToken ct = default);
        Task DeleteAsyncById(K id, CancellationToken ct = default);
        Task DeleteByCondition(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    }
}
