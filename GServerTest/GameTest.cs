using Xunit;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Repository;

namespace GServerTest
{
    public class GameTest
    {
        [Fact]
        public async void Should_Save_Game_To_Db()
        {
            var category = new Category {
                Id = 2,
                Title = "MMORPG"
            };

            var game_root = new GameRoot {
                Id = 1,
                Name = "Sudoku"
            };

            var user = new User {
                FirstName = "Ülkü",
                LastName = "Tester",
                Email = "test@test.com"
            };

            var game = new Game {
                Id = System.Guid.NewGuid(),
                Link = "http://localhost",
                Vote = 10,
                Description = "Description test",
                ImageURL = "http://test_server.com/image.jpg",
                User = user,
                Category = category,
                GameRoot = game_root
            };

            var options = new DbContextOptionsBuilder<GServerDbContext>()
                .UseInMemoryDatabase(databaseName: "test1game")
                .Options;
            
            using(var context = new GServerDbContext(options)) {
                var game_repository = new GameRepository(context);
                var game_root_repository = new GameRootRepository(context);
                var category_repository = new CategoryRepository(context);
                var user_repository = new UserRepository(context);

                await user_repository.AddAsync(user);
                await category_repository.AddAsync(category);
                await game_root_repository.AddAsync(game_root);
                await game_repository.AddAsync(game);
            }

            using (var context = new GServerDbContext(options))
            {
                var repository = new GameRepository(context);
                var all_games = repository.ListAllGames();
                
                Assert.Equal(1, all_games.Count);
                Assert.NotNull(all_games);
                Assert.IsType<Game>(all_games[0]);
            }

        }
    }
}