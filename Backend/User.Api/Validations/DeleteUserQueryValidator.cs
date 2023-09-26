using FluentValidation;
using UserService.Api.Queries;

namespace UserService.Api.Validations
{
    public class DeleteUserQueryValidator : AbstractValidator<DeleteUserQuery>
    {
        public DeleteUserQueryValidator()
        {
            
            //RuleFor(x => x.Id).Must(Validate).WithMessage(m => $"{m.Id} is not a valid Id");
            RuleFor(x => x.Id).NotEmpty().WithMessage("User id cant be null");

        }
    }
}
