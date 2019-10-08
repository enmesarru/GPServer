using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    // : IAsyncRepository<T> where T: Game, IBaseEntity<T>, new() 
    public interface IGameRepository
        : IAsyncRepository<Game, Guid> 
    {
        Task<Game> GetGameWithId(Guid id);
        Task<IReadOnlyList<Game>> ListAllGames();
        Task<bool> PermissionToAddGame(string userId);
        Task<IReadOnlyList<Game>> GetGamesByUserId(string userId);
    }
}