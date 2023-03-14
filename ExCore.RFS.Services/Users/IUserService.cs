using ExCore.RFS.Data;

namespace ExCore.RFS.Services
{
    public interface IUserService
    {
        Task<UserResource> GetUserById(long id);
        Task Create(UserCreate user);
    }
}
