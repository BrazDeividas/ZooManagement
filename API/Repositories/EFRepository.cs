using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        EFRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public ValueTask<T> GetById(int id) => _dbSet.FindAsync(id);

        public async ValueTask<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async ValueTask Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }

        public Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            return _dbContext.SaveChangesAsync();
        }

        public async ValueTask<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}