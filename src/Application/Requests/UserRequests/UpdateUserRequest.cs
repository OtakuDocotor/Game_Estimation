using Domain.Enums;
using FluentValidation;

namespace Application.Requests.UserRequest
{
    public class UpdateUserRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserRoles Role { get; set; }
    }

    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.ID).NotEmpty().ExclusiveBetween(0,int.MaxValue);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.MaxNameLength);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(ValidationConstants.MinLengthPassword);
        }
    }
}
