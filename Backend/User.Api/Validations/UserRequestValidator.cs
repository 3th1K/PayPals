using FluentValidation;
using UserService.Api.Models;

namespace UserService.Api.Validations
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Username).Length(5,50).WithMessage("Username should be of length 5 - 50");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password Should be mimimum length of 8 characters");
            // Add more validation rules as needed
        }
    }
}
