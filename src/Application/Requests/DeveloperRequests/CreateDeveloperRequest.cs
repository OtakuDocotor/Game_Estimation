using FluentValidation;

namespace Application.Requests.DeveloperRequests
{
    public class CreateDeveloperRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoURL { get; set; }
    }

    public class CreateDeveloperRequestValidator : AbstractValidator<CreateDeveloperRequest>
    {
        public CreateDeveloperRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("{PropertyName} has 100 maxlength");
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.LogoURL).MaximumLength(500);
        }
    }
}
