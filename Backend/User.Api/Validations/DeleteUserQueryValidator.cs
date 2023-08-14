using FluentValidation;
using System.Reflection.Metadata.Ecma335;
using UserService.Api.Queries;

namespace UserService.Api.Validations
{
    public class DeleteUserQueryValidator : AbstractValidator<DeleteUserQuery>
    {
        private readonly ILogger<DeleteUserQueryValidator> _logger;
        public DeleteUserQueryValidator(ILogger<DeleteUserQueryValidator> logger)
        {
            _logger = logger;
            _logger.LogInformation("HIIII");
            //RuleFor(x => x.Id).Must(Validate).WithMessage(m => $"{m.Id} is not a valid Id");
            RuleFor(x => x.Id).NotEmpty().WithMessage("User id cant be null");

        }
    }
}
