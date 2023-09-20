using AutoMapper;
using GroupService.Api.Interfaces;
using GroupService.Api.Models;
using Microsoft.EntityFrameworkCore;
using Common.Exceptions;
using Data.Models;
using System.Reflection.Metadata.Ecma335;

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
            var expenses = _context.Expenses.Include(g => g.Users).Where(e => e.GroupId == group.GroupId).ToList();
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
    }
}
