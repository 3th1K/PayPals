using AutoMapper;
using Data.DTOs.ExpenseDTOs;
using Data.Models;
using ExpenseService.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        private async Task<bool> IsUserValid(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == id);
            return user != null;
        }

        public async Task<List<ExpenseResponse>> GetAll()
        {
            var expenses = await _context.Expenses
                .Include(expense => expense.Group)
                .Include(expense => expense.Payer)
                .Include(expense => expense.Users)
                .ToListAsync();
            return _mapper.Map<List<ExpenseResponse>>(expenses);
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

        public async Task<ExpenseResponse> UpdateExpense(ExpenseUpdateRequest request)
        {
            var expenseInDb = await _context.Expenses.SingleOrDefaultAsync(x => x.ExpenseId == request.ExpenseId);
            if (expenseInDb == null) return _mapper.Map<ExpenseResponse>(expenseInDb);
            //if (!await IsUserValid(request.PayerId))
            //{
            //    throw new UserNotFoundException("Provided expense payer is not a registered user");
            //}
            _mapper.Map(request, expenseInDb);
            await _context.SaveChangesAsync();
            var updatedExpenseInDb = await _context.Expenses
                .SingleOrDefaultAsync(expense => expense.ExpenseId == request.ExpenseId);
            return _mapper.Map<ExpenseResponse>(updatedExpenseInDb);
        }

        public async Task<ExpenseResponse> DeleteExpense(int id)
        {
            var expense = await _context.Expenses
                .Include(e=>e.Users)
                .SingleOrDefaultAsync(e => e.ExpenseId == id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<ExpenseResponse>(expense);
        }


        public async Task<ExpenseResponse> GetExpenseDetails(int id) 
        {
            var expense = await _context.Expenses
                .Include(e => e.Users)
                .Include(e => e.Group.Creator)
                .SingleOrDefaultAsync(e => e.ExpenseId == id);
            return _mapper.Map<ExpenseResponse>(expense);
        }

        public async Task<ExpenseResponse> SubmitExpenseApproval(ExpenseApprovalRequest request)
        {
            var expense = await _context.Expenses
                .Include(expense => expense.ExpenseApprovals)
                .FirstOrDefaultAsync(expense => expense.ExpenseId == request.ExpenseId);
            // assuming expense is present
            //var existingApproval = expense.ExpenseApprovals.FirstOrDefault(ea => ea.UserId == request.UserId);
            // assuming this is new approval, not already present
            var addedExpenseApproval = _mapper.Map<ExpenseApproval>(request);
            expense.ExpenseApprovals.Add(addedExpenseApproval);
            expense.ApprovalsReceived = expense.ExpenseApprovals.Count;
            await _context.SaveChangesAsync();
            return _mapper.Map<ExpenseResponse>(expense);
        }

        public async Task<ExpenseApprovalResponse> GetExpenseApprovals(int id)
        {
            var expense = await _context.Expenses
                .Include(expense => expense.ExpenseApprovals)
                .FirstOrDefaultAsync(expense => expense.ExpenseId == id);
            var expenseApprovals = expense.ExpenseApprovals.ToList();
            return _mapper.Map<ExpenseApprovalResponse>(expenseApprovals);
        }
    }
}
