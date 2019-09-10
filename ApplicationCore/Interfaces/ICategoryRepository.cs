using System;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    // class replaced by Category
    public interface ICategoryRepository
        : IAsyncRepository<Category, int>
    {
    }
}