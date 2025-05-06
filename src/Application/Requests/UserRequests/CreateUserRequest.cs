using Domain.Enums;
using FluentValidation;

namespace Application.Requests.UserRequest
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public required string? Email { get; set; }
        public required string? Password { get; set; }
    }

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    { 
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.MaxNameLength);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(ValidationConstants.MinLengthPassword);
        }
    }
}
