using Xunit;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Repository;

namespace GServerTest
{
    public class GameRootTest
    {
        [Fact]
        public async void Should_Save_GameRoot_To_Db() {
            var game_root = new GameRoot { 
                Id = 1,
                Name = "Tavla"
            };

            var options = new DbContextOptionsBuilder<GServerDbContext>()
                .UseInMemoryDatabase("test1root")
                .Options;

            using(var context = new GServerDbContext(options)) {
                var repository = new GameRootRepository(context);
                await repository.AddAsync(game_root);
            }

            using (var context = new GServerDbContext(options))
            {
                var repository = new GameRootRepository(context);
                var all_game_roots = await repository.ListAllAsync();
                Assert.NotNull(all_game_roots);
                Assert.Equal(1, all_game_roots.Count);
            }

        }

        [Fact]
        public async void Should_Get_GameRoot_By_Id() {
            var game_root = new GameRoot { Id = 3, Name = "Sudoku" };

            var options = new DbContextOptionsBuilder<GServerDbContext>()
                .UseInMemoryDatabase(databaseName: "test2root")
                .Options;
            
            using(var context = new GServerDbContext(options)) 
            {
                var repository = new GameRootRepository(context);
                await repository.AddAsync(game_root);
            }

            using (var context = new GServerDbContext(options)) 
            {
                var repository = new GameRootRepository(context);
                var _game_root = await repository.GetByIdAsync(game_root.Id);
                var _game_root_null = await repository.GetByIdAsync(5);
                
                Assert.IsType<GameRoot>(_game_root);
                Assert.Equal("Sudoku", _game_root.Name);
                Assert.Null(_game_root_null);
                Assert.Equal(3, _game_root.Id);
            }
        }

        [Fact]
        public async void Should_Remove_GameRoot_By_Id() {
            var game_root = new GameRoot { Id = 3, Name = "Sudoku" };
            var game_root_2 = new GameRoot { Id = 4, Name = "Chess"};
            var game_root_3 = new GameRoot { Id = 5, Name = "Go" };

            var options = new DbContextOptionsBuilder<GServerDbContext>()
                .UseInMemoryDatabase(databaseName: "test3root")
                .Options;
            
            using(var context = new GServerDbContext(options)) 
            {
                var repository = new GameRootRepository(context);
                await repository.AddAsync(game_root);
                await repository.AddAsync(game_root_2);
                await repository.AddAsync(game_root_3);
            }

            using(var context = new GServerDbContext(options)) 
            {
                var repository = new GameRootRepository(context);
                await repository.DeleteAsync(game_root);
            }


            using (var context = new GServerDbContext(options)) 
            {
                var repository = new GameRootRepository(context);
                var all_game_roots = await repository.ListAllAsync();
                
                Assert.Equal(2, all_game_roots.Count);
            }
        }

        [Fact]
        public async void Should_Update_GameRoot_Data() {
            var game_root = new GameRoot { Id = 1, Name = "Sudoku" };

            var options = new DbContextOptionsBuilder<GServerDbContext>()
                .UseInMemoryDatabase(databaseName: "test4root")
                .Options;
            
            using (var context = new GServerDbContext(options))
            {
                var repository = new GameRootRepository(context);
                await repository.AddAsync(game_root);
            }

            using (var context = new GServerDbContext(options))
            {
                var repository = new GameRootRepository(context);
                var _game_root = await repository.GetByIdAsync(game_root.Id);
                _game_root.Name = "Tavla";
                await repository.UpdateAsync(_game_root);

                _game_root = await repository.GetByIdAsync(game_root.Id);
                Assert.Equal("Tavla", _game_root.Name);
            }
        }
    }
}