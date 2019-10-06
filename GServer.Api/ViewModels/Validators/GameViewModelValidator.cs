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

            RuleFor(game => game.ImageURL);
            RuleFor(game => game.Link).NotNull();
            RuleFor(game => game.Description).NotNull();
            RuleFor(game => game.Name).NotNull();

            RuleFor(game => game.CategoryId).CustomAsync( async (id, context, next) => {
                var isCategoryExist = await ExistsCategory(id);
                if(!isCategoryExist) {
                    context.AddFailure("The category id does not exist.");
                }

            }).NotNull();

            RuleFor(game => game.GameRootId).CustomAsync( async (id, context, next) => {
                var isGameRootExist = await ExistsGameRoot(id);
                if(!isGameRootExist) {
                    context.AddFailure("The game root id does not exist");
                }
            }).NotNull();

            RuleFor(game => game.UserId).CustomAsync( async (id, context, next) => {
                var isUserExist = await ExistsUser(id);
                if(!isUserExist) {
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