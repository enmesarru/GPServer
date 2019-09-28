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
                Title = "TestCategory"
            };

            var game_root = new GameRoot {
                Name = "TestRoot"
            };

            var game = new Game {
                GameRoot = game_root,
                Category = category,
                Link = "http://test.case.com",
                ImageURL = "http://test.case.com/test.jpg",
                Vote = 0,
                Name = "Test Server"
            };
            
        }
    }
}