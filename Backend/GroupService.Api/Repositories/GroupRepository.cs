using AutoMapper;
using GroupService.Api.Interfaces;
using GroupService.Api.Models;
using Microsoft.EntityFrameworkCore;
using Common.Exceptions;
using Data.Models;
using Microsoft.Identity.Client;

namespace GroupService.Api.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GroupRepository(ApplicationDbContext context, IMapper mapper) { 
            
            _context = context;
            _mapper = mapper;

        }
        public async Task<GroupResponse> GetGroupById(int id)
        {
            var group = await _context.Groups.Include(g => g.Users).SingleOrDefaultAsync(g => g.GroupId == id);
            var groupResponse = _mapper.Map<GroupResponse>(group);
            return groupResponse;
        }

        public async Task<List<ExpenseResponse>> GetGroupExpensesById(int id)
        {
            var group = await _context.Groups.SingleOrDefaultAsync(g => g.GroupId == id) ?? throw new GroupNotFoundException();
            var expenses = await _context.Expenses.Include(g => g.Users).Where(e => e.GroupId == group.GroupId).ToListAsync();
            var expensesResponse = _mapper.Map<List<ExpenseResponse>>(expenses);
            return expensesResponse;
        }

        public async Task<bool> CheckUserExistenceInGroup(int groupId, int userId) {

            var user = await  _context.Groups.SingleOrDefaultAsync(g => g.GroupId == groupId && g.Users.Any(u => u.UserId == userId));
            return user != null;
            
        }

        public async Task<Group?> GetGroupByName(string name)
        {
            Group? group = await _context.Groups.FirstOrDefaultAsync(g => g.GroupName == name);
            return group;
        }

        public async Task<GroupResponse> CreateGroup(Group group)
        {
            if (!await IsUserValid(group.CreatorId)) {
                throw new UserNotFoundException("Provided group creator is not a registered user");
            }
            await _context.AddAsync(group);
            await _context.SaveChangesAsync();

            var addedGroup = await _context.Groups.Include(g => g.Users).SingleOrDefaultAsync(g => g.GroupId == group.GroupId);
            return _mapper.Map<GroupResponse>(addedGroup);
        }

        private async Task<bool> IsUserValid(int id) {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == id);
            return user != null;
        }

        public async Task<GroupResponse> UpdateGroup(GroupUpdateRequest groupUpdateRequest)
        {
            var groupInDb = await _context.Groups.SingleOrDefaultAsync(g => g.GroupId == groupUpdateRequest.GroupId);
            if (groupInDb != null)
            {
                if (!await IsUserValid(groupUpdateRequest.CreatorId))
                {
                    throw new UserNotFoundException("Provided group creator is not a registered user");
                }
                _mapper.Map(groupUpdateRequest, groupInDb);
                await _context.SaveChangesAsync();
                var updatedGroupInDb = await _context.Groups.Include(g => g.Users).SingleOrDefaultAsync(g => g.GroupId == groupUpdateRequest.GroupId);
                return _mapper.Map<GroupResponse>(updatedGroupInDb);
            }
            return _mapper.Map<GroupResponse>(groupInDb);
        }

        public async Task<List<GroupResponse>> GetAllGroups()
        {
            var groups = await _context.Groups.Include(g => g.Creator).Include(g=>g.Users).ToListAsync();
            var groupResponses = _mapper.Map<List<GroupResponse>>(groups);
            return groupResponses;
        }

        public async Task<GroupResponse> DeleteGroup(int id)
        {
            var group = await _context.Groups.SingleOrDefaultAsync(x => x.GroupId == id);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<GroupResponse>(group);
        }

        public async Task<GroupResponse> AddMemberInGroup(int groupId, int userId)
        {
            var group = await _context.Groups.Include(g => g.Users).SingleOrDefaultAsync(g => g.GroupId == groupId);
            if (group != null)
            {
                if (!await IsUserValid(userId)) {
                    throw new UserNotFoundException("Provided user does not exists");
                }
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
                if (user != null)
                {
                    group.Users.Add(user);
                    await _context.SaveChangesAsync();
                }                
            }
            return _mapper.Map<GroupResponse>(group);
        }

        public async Task<GroupResponse> DeleteMemberFromGroup(int groupId, int userId)
        {
            var group = await _context.Groups.Include(g => g.Users).SingleOrDefaultAsync(g => g.GroupId == groupId);
            if (group != null)
            {
                if (!await CheckUserExistenceInGroup(group.GroupId, userId))
                {
                    throw new UserNotFoundException("Provided user does not exists in the group");
                }
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
                if (user != null)
                {
                    group.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
                
            }
            return _mapper.Map<GroupResponse>(group);
        }
    }
}
