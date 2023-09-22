using FluentValidation;
using GroupService.Api.Models;

namespace GroupService.Api.Validations
{
    public class GroupRequestValidator : AbstractValidator<GroupRequest>
    {
        public GroupRequestValidator()
        {
            RuleFor(g => g.GroupName).MaximumLength(20).WithMessage("Group Name Cannot Exceed 20 characters");
        }
    }
}
