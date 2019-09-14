using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GameRepository : EfRepository<Game, Guid>, IGameRepository
    {
        public GameRepository(GServerDbContext gServerDbContext) : base(gServerDbContext)
        {
        }

        public async Task<IReadOnlyList<Game>> ListAllGames()
        {
            return await _gServerDbContext.Games
                .Include(g => g.GameRoot)
                .Include(c => c.Category)
                .Include(u => u.User)
                .ToListAsync();
        }

        public async Task<Game> GetGameWithId(Guid id)
        {
            return await _gServerDbContext.Games.Where(x => x.Id == id)
                .Include(g => g.GameRoot)
                .Include(c => c.Category)
                .Include(u => u.User)
                .FirstOrDefaultAsync();
        }
    }
}