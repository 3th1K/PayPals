using FluentValidation;
using GroupService.Api.Queries;

namespace GroupService.Api.Validations
{
    public class GetGroupExpensesByIdQueryValidator : AbstractValidator<GetGroupExpensesByIdQuery>
    {
        public GetGroupExpensesByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Group Id can't be empty");
        }
    }
}
