using FluentValidation;
using GroupService.Api.Queries;

namespace GroupService.Api.Validations
{
    public class GetGroupByIdQueryValidator : AbstractValidator<GetGroupByIdQuery>
    {
        public GetGroupByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).WithMessage("Group Id can't be less than 1");
            RuleFor(x => x.Id).NotNull().WithMessage("Group Id can't be null");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Group Id can't be empty");
        }
    }
}
