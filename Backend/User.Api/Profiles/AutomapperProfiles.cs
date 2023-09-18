using AutoMapper;
using Data.Models;
using UserService.Api.Models;

namespace UserService.Api.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserRequest,User>();
            CreateMap<UserUpdateRequest, User>();
            CreateMap<User, UserRequest>();
        }
    }
}
