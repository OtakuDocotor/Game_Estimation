using FluentValidation;

namespace Application.Requests.UserRequest
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
    }

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    { 
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.MaxNameLength);
        }
    }
}
