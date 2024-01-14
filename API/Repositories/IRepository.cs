using System.Linq.Expressions;

namespace API.Repositories

{
    public interface IRepository<T> where T : class
    {
        ValueTask<IEnumerable<T>> GetAll();
        ValueTask<T> GetById(int id);
        ValueTask<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        ValueTask Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}