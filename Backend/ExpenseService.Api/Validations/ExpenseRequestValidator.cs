using ExpenseService.Api.Models;
using FluentValidation;

namespace ExpenseService.Api.Validations
{
    public class ExpenseRequestValidator : AbstractValidator<ExpenseRequest>
    {
        public ExpenseRequestValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount Cannot be 0");
        }
    }
}
