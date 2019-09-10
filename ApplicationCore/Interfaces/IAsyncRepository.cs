using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IAsyncRepository<RType, EntityType> 
        where RType: class,
        IBaseEntity<EntityType>
    {
        Task<RType> GetByIdAsync(EntityType id);
        Task<IReadOnlyList<RType>> ListAllAsync();
        Task<IReadOnlyList<RType>> ListAsync(Expression<Func<RType, bool>> expression);
        Task AddAsync(RType entity);
        Task UpdateAsync(RType entity);
        Task DeleteAsync(RType entity);
        Task<int> CountAsync();
    }
}