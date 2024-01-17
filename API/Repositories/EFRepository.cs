using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly ZooDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EFRepository(ZooDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async ValueTask<T> GetById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if(entity != null)
            {
                return entity;
            }
            
            throw new Exception("Entity not found");
        }

        public async ValueTask<T> GetOneWithRelatedDataAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.Where(predicate).AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync();
        }

        public async ValueTask<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async ValueTask<List<T>> GetAllWithRelatedDataAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async ValueTask Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
        }

        public async ValueTask AddRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
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