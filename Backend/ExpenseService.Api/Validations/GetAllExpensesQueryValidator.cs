using ExpenseService.Api.Queries;
using FluentValidation;

namespace ExpenseService.Api.Validations
{
    public class GetAllExpensesQueryValidator : AbstractValidator<GetAllExpensesQuery>
    {
        public GetAllExpensesQueryValidator()
        {
            
        }
    }
}
