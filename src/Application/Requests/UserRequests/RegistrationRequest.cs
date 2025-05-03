using FluentValidation;

namespace Application.Requests.UserRequests
{
    public class RegistrationRequest
    {
        public string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
    {
        public RegistrationRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.MaxNameLength);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
