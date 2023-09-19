using FluentValidation;
using UserService.Api.Models;
using UserService.Api.Queries;

namespace UserService.Api.Validations
{
    public class GetUserGroupsQueryValidator : AbstractValidator<GetUserGroupsQuery>
    {
        public GetUserGroupsQueryValidator()
        {
            //RuleFor(x => x.Id).NotEmpty().WithMessage("User Id can't be empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid User Id");
            //RuleFor(x => x.Id).Must(ValidateGuid).WithMessage("Not A Valid User Id");
        }
        //private bool ValidateGuid(string? id)
        //{
        //    return Guid.TryParse(id, out _);
        //}
    }
}
