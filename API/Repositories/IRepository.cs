using System.Linq.Expressions;

namespace API.Repositories

{
    public interface IRepository<T> where T : class
    {
        ValueTask<IEnumerable<T>> GetAll();
        ValueTask<T> GetOneWithRelatedDataAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        ValueTask<List<T>> GetAllWithRelatedDataAsync(params Expression<Func<T, object>>[] includes);
        ValueTask<T> GetById(int id);
        ValueTask<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        ValueTask Add(T entity);
        ValueTask AddRange(IEnumerable<T> entities);
        Task Update(T entity);
        Task Delete(T entity);
    }
}