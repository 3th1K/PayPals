using ExpenseService.Api.Queries;
using FluentValidation;

namespace ExpenseService.Api.Validations;

public class DeleteExpenseParticipantValidator : AbstractValidator<DeleteExpenseParticipantQuery>
{
    public DeleteExpenseParticipantValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("Invalid User Id");
        RuleFor(x => x.ExpenseId).GreaterThan(0).WithMessage("Invalid Expense Id");
    }
}