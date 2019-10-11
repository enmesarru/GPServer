using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GameRepository : EfRepository<Game, Guid>, IGameRepository
    {
        private readonly UserManager<User> userManager;
        public GameRepository(
            GServerDbContext gServerDbContext,
            UserManager<User> userManager) 
            : base(gServerDbContext)
        {
            this.userManager = userManager;
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

        public async Task<bool> PermissionToAddGame(string userId)
        {
            var lastGameDate = await _gServerDbContext.Games
                .Where(x => x.UserId == userId)
                .Select(x => x.CreatedOn)
                .OrderByDescending(t => t.Date)
                .ThenBy(t => t.TimeOfDay)
                .FirstOrDefaultAsync();
            int result = lastGameDate.Date.CompareTo(DateTime.Now.Date);
            return result == 0 ? false : true;
        }

        public async Task<IReadOnlyList<Game>> GetGamesByUserName(string username) 
        { 
            var user = await userManager.FindByNameAsync(username);
            return await _gServerDbContext.Games
                .Where(x => x.UserId == user.Id)
                .Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.GameRoot)
                .ToListAsync();
        }
    }
}