using ExCore.RFS.Data;

namespace ExCore.RFS.Services
{
    public class UserService : IUserService
    {
        private RFSBaseService _baseService;
        public UserService(RFSBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task Create(UserCreate user)
        {
            await _baseService.CreateAsync<User, UserCreate>(user);
        }

        public async Task<UserResource> GetUserById(long id)
        {
            return await _baseService.FindAsync<User, UserResource>(id);
        }
    }
}
