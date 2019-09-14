using FluentValidation;

namespace GServer.Api.ViewModels.Validators
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(user => user.Email).NotNull();
            RuleFor(user => user.Password).NotNull();
            RuleFor(user => user.Username).NotNull();
        }
    }
}