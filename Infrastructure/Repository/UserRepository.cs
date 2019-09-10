using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class UserRepository : EfRepository<User, string>, IUserRepository
    {
        public UserRepository(GServerDbContext gServerDbContext) : base(gServerDbContext)
        {
        }
    }
}