using Microsoft.EntityFrameworkCore;
using simple_leave_management_system.Infrastructure.Contracts;
using System.Linq.Expressions;

namespace simple_leave_management_system.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext DbContext { get; set; }
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            DbContext = repositoryContext;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>().Where(expression).AsNoTracking().FirstAsync() != null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Set<T>().Update(entity);
            await DbContext.SaveChangesAsync();
        }

        Task IRepositoryBase<T>.CreateAsync(T entity)
        {
            return CreateAsync(entity);
        }

        public async Task<T?> FindOneAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>().AsNoTracking().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbContext.Set<T>();

            foreach (var include in includes) query = query.Include(include);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbContext.Set<T>().Where(expression);

            foreach (var include in includes) query = query.Include(include);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T?> FindOneAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbContext.Set<T>().Where(expression);

            foreach (var include in includes) query = query.Include(include);

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
