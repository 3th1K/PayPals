using AutoMapper;
using UserService.Api.Models;

namespace UserService.Api.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserRequest,User>();
            CreateMap<User, UserRequest>();
        }
    }
}
