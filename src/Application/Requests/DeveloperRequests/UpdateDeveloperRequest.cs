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
            RuleFor(x => x.ID).NotEmpty().ExclusiveBetween(0, int.MaxValue);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.MaxNameLenght).WithMessage("{PropertyName} has 100 maxlength");
            RuleFor(x => x.Description).MaximumLength(ValidationConstants.MaxDescriptionLength);
            RuleFor(x => x.LogoURL).MaximumLength(500);
        }
    }
}
