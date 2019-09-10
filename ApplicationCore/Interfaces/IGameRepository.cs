using System;
using System.Collections;
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
        IReadOnlyList<Game> ListAllGames();
    }
}