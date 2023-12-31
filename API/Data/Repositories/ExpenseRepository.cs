﻿using AutoMapper;
using Common.DTOs.ExpenseDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
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
                .Include(expense => expense.ExpenseApprovals)
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
                .Include(expense => expense.ExpenseApprovals)
                .SingleOrDefaultAsync(e => e.ExpenseId == id);
            return _mapper.Map<ExpenseResponse>(expense);
        }

        public async Task<ExpenseResponse> SubmitExpenseApproval(ExpenseApprovalRequest request)
        {
            var expense = await _context.Expenses
                .Include(e => e.Payer)
                .Include(expense => expense.ExpenseApprovals)
                .FirstOrDefaultAsync(expense => expense.ExpenseId == request.ExpenseId);
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

        public async Task<ExpenseStatusResponse> GetExpenseStatus(int id)
        {
            var expense = await _context.Expenses
                .Include(expense => expense.ExpenseApprovals)
                .SingleOrDefaultAsync(expense => expense.ExpenseId == id);
            if (expense == null)
            {
                throw new ExpenseNotFoundException();
            }

            var status = ExpenseStatus.Active;
            var approvalsReceived = expense.ApprovalsReceived;
            var totalMembers = expense.TotalMembers;
            var totalNumberOfApproved = expense.ExpenseApprovals.Count(ea => ea.IsApproved);
            if (approvalsReceived < totalMembers)
            {
                status = ExpenseStatus.Active;
            }
            else if (totalNumberOfApproved == totalMembers)
            {
                status = ExpenseStatus.Approved;
            }
            else if (approvalsReceived == totalMembers && totalNumberOfApproved < totalMembers)
            {
                status = ExpenseStatus.Rejected;
            }

            return new ExpenseStatusResponse
            {
                ExpenseId = expense.ExpenseId,
                TotalMembers = totalMembers,
                ApprovalReceived = approvalsReceived,
                ExpenseApprovals = _mapper.Map<List<ExpenseApprovalResponse>>(expense.ExpenseApprovals.ToList()),
                Status = status
            };
        }

        public async Task<ExpenseResponse> AddParticipant(int expenseId, int userId)
        {
            var expense = await _context.Expenses
                              .Include(expense => expense.Users)
                              .SingleOrDefaultAsync(expense => expense.ExpenseId == expenseId)
                          ??throw new ExpenseNotFoundException("Expense Not Found in Database");
            var user = await _context.Users.SingleOrDefaultAsync(user => user.UserId == userId)
                       ??throw new UserNotFoundException("User not found in the database");
            var isUserAlreadyInExpense = expense.Users.Any(u => u.UserId == userId);
            if (isUserAlreadyInExpense)
            {
                throw new UserAlreadyExistsException("User already exists in this expense");
            }
            expense.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<ExpenseResponse>(expense);
        }

        public async Task<ExpenseResponse> DeleteParticipant(int expenseId, int userId)
        {
            var expense = await _context.Expenses
                              .Include(expense => expense.Users)
                              .SingleOrDefaultAsync(expense => expense.ExpenseId == expenseId)
                          ?? throw new ExpenseNotFoundException("Expense Not Found in Database");
            var user = await _context.Users.SingleOrDefaultAsync(user => user.UserId == userId)
                       ?? throw new UserNotFoundException("User not found in the database");
            var isUserAlreadyInExpense = expense.Users.Any(u => u.UserId == userId);
            if (!isUserAlreadyInExpense)
            {
                throw new UserNotFoundException("User does not exists in this expense");
            }
            expense.Users.Remove(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<ExpenseResponse>(expense);
        }
    }
}
