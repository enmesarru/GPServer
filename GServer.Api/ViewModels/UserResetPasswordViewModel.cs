namespace GServer.Api.ViewModels
{
    public class UserResetPasswordViewModel
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}