using Xunit;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Repository;

namespace GServerTest
{
    public class CategoryTest
    {
        [Fact]
        public async void Should_Save_Category_To_Db()
        {
            var category = new Category { Id = 1, Title = "MMORPG" };
            var category_2 = new Category { Id = 2, Title = "FPS" };

            var options = new DbContextOptionsBuilder<GServerDbContext>()
                .UseInMemoryDatabase(databaseName: "test1")
                .Options;

            using(var context = new GServerDbContext(options)) {
                var repository = new CategoryRepository(context);
                await repository.AddAsync(category);
                await repository.AddAsync(category_2);
            }

            using (var context = new GServerDbContext(options))
            {
                var repository = new CategoryRepository(context);
                var all_categories = await repository.ListAllAsync();
                Assert.NotNull(all_categories);
            }
        }

        [Fact]
        public async void Should_Get_Category_By_Id() {
            var category = new Category { Id = 3, Title = "MMORPG" };
            var category_2 = new Category { Id = 4, Title = "FPS" };

            var options = new DbContextOptionsBuilder<GServerDbContext>()
                .UseInMemoryDatabase(databaseName: "test2")
                .Options;
            
            using(var context = new GServerDbContext(options)) 
            {
                var repository = new CategoryRepository(context);
                await repository.AddAsync(category);
                await repository.AddAsync(category_2);
            }

            using (var context = new GServerDbContext(options)) 
            {
                var repository = new CategoryRepository(context);
                var _category = await repository.GetByIdAsync(category.Id);
                var _category_null = await repository.GetByIdAsync(5);
                
                Assert.IsType<Category>(_category);
                Assert.Equal("MMORPG", _category.Title);
                Assert.Null(_category_null);
                Assert.Equal(3, _category.Id);
            }
        }

        [Fact]
        public async void Should_Remove_Category_Data_By_Id() {
            var category = new Category { Id = 3, Title = "MMORPG" };
            var category_2 = new Category { Id = 4, Title = "FPS" };
            var category_3 = new Category { Id = 5, Title = "FPS" };

            var options = new DbContextOptionsBuilder<GServerDbContext>()
                .UseInMemoryDatabase(databaseName: "test3")
                .Options;
            
            using(var context = new GServerDbContext(options)) 
            {
                var repository = new CategoryRepository(context);
                await repository.AddAsync(category);
                await repository.AddAsync(category_2);
                await repository.AddAsync(category_3);
            }

            using(var context = new GServerDbContext(options)) 
            {
                var repository = new CategoryRepository(context);
                await repository.DeleteAsync(category);
            }


            using (var context = new GServerDbContext(options)) 
            {
                var repository = new CategoryRepository(context);
                var all_categories = await repository.ListAllAsync();
                
                Assert.Equal(2, all_categories.Count);
            }
        }

        [Fact]
        public async void Should_Update_Category_Data() {
            var category = new Category { Id = 1, Title = "FPS" };

            var options = new DbContextOptionsBuilder<GServerDbContext>()
                .UseInMemoryDatabase(databaseName: "test4")
                .Options;
            
            using (var context = new GServerDbContext(options))
            {
                var repository = new CategoryRepository(context);
                await repository.AddAsync(category);
            }

            using (var context = new GServerDbContext(options))
            {
                var repository = new CategoryRepository(context);
                var _category = await repository.GetByIdAsync(category.Id);
                _category.Title = "MMORPG";
                await repository.UpdateAsync(_category);

                _category = await repository.GetByIdAsync(category.Id);
                Assert.Equal("MMORPG", _category.Title);
            }
        }
    }
}
