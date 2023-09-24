using FluentValidation;
using UserService.Api.Queries;

namespace UserService.Api.Validations
{
    public class GetUserExpensesQueryValidator : AbstractValidator<GetUserExpensesQuery>
    {
        public GetUserExpensesQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid User Id");
        }
    }
}
