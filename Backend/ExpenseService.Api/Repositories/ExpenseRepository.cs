using AutoMapper;
using ExpenseService.Api.Interfaces;
using ExpenseService.Api.Models;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ExpenseService.Api.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ExpenseRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }
        public async Task<ExpenseResponse> CreateExpense(ExpenseRequest request)
        {

            var newExpense = _mapper.Map<Expense>(request);

            var users = await _context.Users
                .Where(u => request.UserIds.Contains(u.UserId))
                .ToListAsync();

            foreach (var user in users)
            {
                newExpense.Users.Add(user);
            }

            await _context.Expenses
                .AddAsync(newExpense);
            
            await _context.SaveChangesAsync();
            var addedExpense = await _context.Expenses
                .Include(e=>e.Group.Creator)
                .SingleOrDefaultAsync(e => e.ExpenseId == newExpense.ExpenseId);

            

            return _mapper.Map<ExpenseResponse>(addedExpense);
        }

        public async Task<ExpenseResponse> GetExpenseDetails(int id) 
        {
            var expense = await _context.Expenses
                .Include(e => e.Users)
                .Include(e => e.Group.Creator)
                .SingleOrDefaultAsync(e => e.ExpenseId == id);
            return _mapper.Map<ExpenseResponse>(expense);
        }
    }
}
