using AutoMapper;
using GroupService.Api.Interfaces;
using GroupService.Api.Models;
using Microsoft.EntityFrameworkCore;
using Common.Exceptions;
using Data.Models;

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
    }
}
