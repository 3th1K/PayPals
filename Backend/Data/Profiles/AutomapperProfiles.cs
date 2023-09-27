﻿using AutoMapper;
using Data.DTOs.ExpenseDTOs;
using Data.DTOs.GroupDTOs;
using Data.DTOs.UserDTOs;
using Data.Models;

namespace Data.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserResponse, User>();
            CreateMap<User, UserDetailsResponse>();
            CreateMap<Expense, UserDetailsExpenseResponse>();
            CreateMap<Group, UserDetailsGroupResponse>();
            CreateMap<UserRequest,User>();
            CreateMap<UserUpdateRequest, User>();
            CreateMap<User, UserRequest>();
            

            CreateMap<Group, GroupResponse>();
            CreateMap<GroupRequest, GroupResponse>();
            CreateMap<GroupRequest, Group>();
            CreateMap<GroupUpdateRequest, Group>();
            CreateMap<GroupResponse, Group>();
            

            CreateMap<ExpenseRequest, Expense>()
                //.ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserIds.Select(id => new User { UserId = id })))
                .ForMember(expense => expense.TotalMembers, opt => opt.MapFrom(src => src.UserIds.Count))
                .ForMember(expense => expense.Users, opt => opt.Ignore());
            CreateMap<ExpenseUpdateRequest, Expense>();
            CreateMap<Expense, ExpenseResponse>();
            
            
        }
    }
}
