using Common.DTOs.ExpenseDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using MediatR;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetUserExpensesQueryHandler : IRequestHandler<GetUserExpensesQuery, ApiResult<List<ExpenseResponse>>>
    {

        private readonly IUserRepository _userRepository;
        public GetUserExpensesQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }
        public async Task<ApiResult<List<ExpenseResponse>>> Handle(GetUserExpensesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id) ?? throw new UserNotFoundException("This User is not a valid User");
            if (user == null)
            {
                return ApiResult<List<ExpenseResponse>>.Failure(ErrorType.ErrUserNotFound, "This User is not a valid User");
            }

            var expenses = await _userRepository.GetUserExpenses(request.Id);
            return ApiResult<List<ExpenseResponse>>.Success(expenses);
        }
    }
}
