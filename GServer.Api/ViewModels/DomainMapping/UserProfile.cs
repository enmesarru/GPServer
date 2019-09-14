using ApplicationCore.Entities;
using AutoMapper;

namespace GServer.Api.ViewModels.DomainMapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();
        }
    }
}