using ApplicationCore.Entities;
using AutoMapper;

namespace GServer.Api.ViewModels.DomainMapping
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameViewModel, Game>();
            CreateMap<Game, GameViewModel>();

            CreateMap<GamesViewModel, Game>();
            CreateMap<Game, GamesViewModel>();

            CreateMap<UserInformationViewModel, User>();
            CreateMap<User, UserInformationViewModel>();
        }
    }
}