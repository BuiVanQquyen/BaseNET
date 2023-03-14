using AutoMapper;
using ExCore.RFS.Data;

namespace ExCore.RFS.Services
{
    public class UserDtoToEntity : Profile
    {
        public UserDtoToEntity()
        {
            CreateMap<UserResource, User>();
            CreateMap<UserCreate, User>();
        }
    }

    public class UserEntityToDto : Profile
    {
        public UserEntityToDto()
        {
            CreateMap<User, UserResource>();
        }
    }
}
