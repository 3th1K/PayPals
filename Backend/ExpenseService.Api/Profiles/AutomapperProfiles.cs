using AutoMapper;
using ExpenseService.Api.Models;

namespace ExpenseService.Api.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<ExpenseRequest, Expense>()
                //.ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserIds.Select(id => new User { UserId = id })))
                .ForMember(dest => dest.TotalMembers, opt => opt.MapFrom(src => src.UserIds.Count))
                .ForMember(dest => dest.Users, opt => opt.Ignore());
            CreateMap<Expense, ExpenseResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<Group, GroupResponse>();
        }
    }
}
