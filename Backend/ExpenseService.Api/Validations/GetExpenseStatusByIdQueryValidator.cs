﻿using ExpenseService.Api.Queries;
using FluentValidation;

namespace ExpenseService.Api.Validations;

public class GetExpenseStatusByIdQueryValidator : AbstractValidator<GetExpenseStatusByIdQuery>
{
    public GetExpenseStatusByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThanOrEqualTo(0).WithMessage("Expense Id can't be less than 1");
        RuleFor(x => x.Id).NotNull().WithMessage("Expense Id can't be null");
        RuleFor(x => x.Id).NotEmpty().WithMessage("Expense Id can't be empty");
    }
}