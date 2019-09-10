using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class GameRootRepository : EfRepository<GameRoot, int>, IGameRootRepository
    {
        public GameRootRepository(GServerDbContext gServerDbContext) : base(gServerDbContext)
        {
        }
    }
}