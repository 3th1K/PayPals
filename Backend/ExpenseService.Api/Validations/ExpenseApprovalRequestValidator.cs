using Common.DTOs.ExpenseDTOs;
using FluentValidation;

namespace ExpenseService.Api.Validations;

public class ExpenseApprovalRequestValidator : AbstractValidator<ExpenseApprovalRequest>
{
    public ExpenseApprovalRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User id can not be empty")
            .GreaterThan(0).WithMessage("User id can not be less than 0");
        RuleFor(x => x.IsApproved)
            .NotNull().WithMessage("IsApproved field cannot be null")
            .Must(BeValidBoolean).WithMessage("IsApproved must be a valid boolean value");
    }
    private bool BeValidBoolean(bool? value)
    {
        // Ensure that the value is either true, false, or null (if you want to allow null).
        return value == true || value == false;
    }
}