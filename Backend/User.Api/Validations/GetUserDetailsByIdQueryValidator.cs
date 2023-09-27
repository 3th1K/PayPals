using FluentValidation;
using UserService.Api.Queries;

namespace UserService.Api.Validations
{
    public class GetUserDetailsByIdQueryValidator : AbstractValidator<GetUserDetailsByIdQuery>
    {
        public GetUserDetailsByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("User Id can't be empty");
        }
    }
}
