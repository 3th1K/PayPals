using Common.DTOs.GroupDTOs;
using FluentValidation;

namespace GroupService.Api.Validations
{
    public class GroupUpdateRequestValidator : AbstractValidator<GroupUpdateRequest>
    {
        public GroupUpdateRequestValidator()
        {
            RuleFor(g => g.GroupName).MaximumLength(20).WithMessage("Group name cannot exceed 20 characters");
            RuleFor(g => g.TotalExpenses).GreaterThanOrEqualTo(0).WithMessage("Group expenses can never be in negetive");
        }
    }
}
