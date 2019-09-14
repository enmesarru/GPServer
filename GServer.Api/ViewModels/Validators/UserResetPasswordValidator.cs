using FluentValidation;

namespace GServer.Api.ViewModels.Validators
{
    public class UserResetPasswordValidator : AbstractValidator<UserResetPasswordViewModel>
    {
        public UserResetPasswordValidator()
        {
            RuleFor(user => user.OldPassword).NotNull();
            RuleFor(user => user.NewPassword).NotNull();
            RuleFor(user => user.Username).NotNull();
        }
    }
}