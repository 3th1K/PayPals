using AutoMapper;
using Data.Models;
using GroupService.Api.Models;

namespace GroupService.Api.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Group, GroupResponse>();
            CreateMap<GroupRequest, GroupResponse>();
            CreateMap<GroupRequest, Group>();
            CreateMap<UserResponse, User>();
            CreateMap<GroupResponse, Group>();
            CreateMap<Expense, ExpenseResponse>();
        }
    }
}
