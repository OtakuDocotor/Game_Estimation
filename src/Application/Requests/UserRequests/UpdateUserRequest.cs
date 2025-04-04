using FluentValidation;

namespace Application.Requests.UserRequest
{
    public class UpdateUserRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.ID).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("{PropertyName} has 100 maxlength");
        }
    }

}
