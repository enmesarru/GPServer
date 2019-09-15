using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace GServer.Api.ViewModels.Validators
{
    public class GameViewModelValidator : AbstractValidator<GameViewModel>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IGameRootRepository gameRootRepository;
        private readonly UserManager<User> userManager;
        public GameViewModelValidator(
            ICategoryRepository categoryRepository,
            IGameRootRepository gameRootRepository,
            UserManager<User> userManager)
        {
            this.categoryRepository = categoryRepository;
            this.gameRootRepository = gameRootRepository;
            this.userManager = userManager;

            RuleFor(game => game.ImageURL).Null();
            RuleFor(game => game.Link).NotNull();
            RuleFor(game => game.Description).NotNull();

            RuleFor(game => game.CategoryId).CustomAsync( async (id, context, next) => {
                var is_category_exist = await ExistsCategory(id);
                if(!is_category_exist) {
                    context.AddFailure("The category id does not exist.");
                }

            }).NotNull();

            RuleFor(game => game.GameRootId).CustomAsync( async (id, context, next) => {
                var is_gameroot_exist = await ExistsGameRoot(id);
                if(!is_gameroot_exist) {
                    context.AddFailure("The game root id does not exist");
                }
            }).NotNull();

            RuleFor(game => game.UserId).CustomAsync( async (id, context, next) => {
                var is_user_exist = await ExistsUser(id);
                if(!is_user_exist) {
                    context.AddFailure("The user id does not exist");
                }
            }).NotNull();
        }

        private async Task<bool> ExistsCategory(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            return category is null ? false : true;
        }
        private async Task<bool> ExistsGameRoot(int id)
        {
            var game_root = await gameRootRepository.GetByIdAsync(id);
            return game_root is null ? false : true;
        }

        private async Task<bool> ExistsUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user is null ? false : true;
        }
    }
}