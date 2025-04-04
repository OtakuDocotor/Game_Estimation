using FluentValidation;

namespace Application.Requests.DeveloperRequests
{
    public class UpdateDeveloperRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoURL { get; set; }
    }

    public class UpdateDeveloperRequestValidator : AbstractValidator<UpdateDeveloperRequest>
    {
        public UpdateDeveloperRequestValidator()
        {
            RuleFor(x => x.ID).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("{PropertyName} has 100 maxlength");
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.LogoURL).MaximumLength(500);
        }
    }
}
