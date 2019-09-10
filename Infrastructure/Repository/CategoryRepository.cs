using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class CategoryRepository : EfRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(GServerDbContext gServerDbContext) : base(gServerDbContext)
        {
        }
    }
}