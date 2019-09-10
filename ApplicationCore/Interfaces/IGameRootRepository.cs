using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IGameRootRepository
        : IAsyncRepository<GameRoot, int>
    {
        
    }
}