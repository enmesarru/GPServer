using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /*
        @T: Type of Entity class
        @U: Type of IBaseEntity class's id type
    */
    public class EfRepository<T, U>: 
        IAsyncRepository<T, U> where T : class,
        IBaseEntity<U>
    {
        protected readonly GServerDbContext _gServerDbContext;
        public EfRepository(GServerDbContext gServerDbContext)
        {
            this._gServerDbContext = gServerDbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _gServerDbContext.Set<T>().AddAsync(entity);
            await _gServerDbContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync() 
            => await _gServerDbContext.Set<T>().CountAsync();

        public Task DeleteAsync(T entity)
        {
            _gServerDbContext.Set<T>().Remove(entity);
            return _gServerDbContext.SaveChangesAsync();
        }

        public Task<T> GetByIdAsync(U id)
            => _gServerDbContext.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> ListAllAsync() 
            => await _gServerDbContext.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> expression) 
            => await _gServerDbContext.Set<T>().Where(expression).ToListAsync();

        public Task UpdateAsync(T entity)
        {
            _gServerDbContext.Entry(entity).State = EntityState.Modified;
            return _gServerDbContext.SaveChangesAsync();
        }
    }
}