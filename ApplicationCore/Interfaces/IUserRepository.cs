using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository
        : IAsyncRepository<User, string>
    {

    }
}