using FluentValidation;
using UserService.Api.Models;

namespace UserService.Api.Validations
{
    public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateRequestValidator()
        {
            foreach (var property in typeof(UserUpdateRequest).GetProperties())
            {
                RuleFor(x => property.GetValue(x))
                    .NotNull().WithMessage($"{property.Name} is missing from the request")
                    .NotEmpty().WithMessage($"{property.Name} should not be empty");    
            }
            RuleFor(x => x.Username).Length(5, 50).WithMessage("Username should be of length 5 - 50");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password Should be mimimum length of 8 characters");
        }
    }
}
