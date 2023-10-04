using Common.DTOs.ExpenseDTOs;
using FluentValidation;

namespace ExpenseService.Api.Validations
{
    public class ExpenseUpdateRequestValidator : AbstractValidator<ExpenseUpdateRequest>
    {
        public ExpenseUpdateRequestValidator()
        {
            RuleFor(e => e.ExpenseId)
                .NotEmpty().WithMessage("Expense id cannot be empty")
                .GreaterThan(0).WithMessage("Expense id cannot be less than or equal to 0");

            RuleFor(e => e.Amount)
                .GreaterThan(0).WithMessage("Amount cannot be less than or equal to 0");

            RuleFor(e => e.Description)
                .MinimumLength(1).WithMessage("Description cannot be less than 1 characters")
                .MaximumLength(50).WithMessage("Description cannot exceed 50 characters");
        }
    }
}
