using FluentValidation;
using GroupService.Api.Queries;

namespace GroupService.Api.Validations
{
    public class DeleteMemberQueryValidator : AbstractValidator<DeleteMemberQuery>
    {
        public DeleteMemberQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("Invalid User Id");
            RuleFor(x => x.GroupId).GreaterThan(0).WithMessage("Invalid Group Id");
        }
    }
}
