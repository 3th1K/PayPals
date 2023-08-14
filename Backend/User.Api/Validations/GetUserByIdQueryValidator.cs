using FluentValidation;
using UserService.Api.Models;
using UserService.Api.Queries;

namespace UserService.Api.Validations
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("User Id can't be empty");
            //RuleFor(x => x.Id).Must(ValidateGuid).WithMessage("Not A Valid User Id");
        }
        //private bool ValidateGuid(string? id)
        //{
        //    return Guid.TryParse(id, out _);
        //}
    }
}
