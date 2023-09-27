using Common.Exceptions;
using Data.DTOs.ExpenseDTOs;
using MediatR;
using UserService.Api.Interfaces;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetUserExpensesQueryHandler : IRequestHandler<GetUserExpensesQuery, List<ExpenseResponse>>
    {

        private readonly IUserRepository _userRepository;
        public GetUserExpensesQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }
        public async Task<List<ExpenseResponse>> Handle(GetUserExpensesQuery request, CancellationToken cancellationToken)
        {
            _ = await _userRepository.GetUserById(request.Id) ?? throw new UserNotFoundException("This User is not a valid User");
            var expenses = await _userRepository.GetUserExpenses(request.Id);
            return expenses;
        }
    }
}
