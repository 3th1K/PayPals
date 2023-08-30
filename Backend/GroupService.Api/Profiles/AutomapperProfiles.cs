using AutoMapper;
using GroupService.Api.Models;

namespace GroupService.Api.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Group, GroupResponse>();
            CreateMap<UserResponse, User>();
            CreateMap<GroupResponse, Group>();
        }
    }
}
