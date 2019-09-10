using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GServerDbContext : IdentityDbContext<User, AppIdentityRole, string>
    {
        public GServerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameRoot> GameRoots { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}