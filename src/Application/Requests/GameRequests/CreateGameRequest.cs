using FluentValidation;

namespace Application.Requests.GameRequests
{
    public class CreateGameRequest
    {
        public string Name { get; set; }
        public double AverageScore { get; set; }
        public int DeveloperId { get; set; }
    }

    public class CreateGameRequestValidator : AbstractValidator<CreateGameRequest>
    {
        public CreateGameRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("{PropertyName} has 100 maxlength");
            RuleFor(x => x.AverageScore).GreaterThan(0).WithMessage("AVG score must be greater than zero").LessThanOrEqualTo(10).WithMessage("AVG score must be less or equal to 10");
            RuleFor(x => x.DeveloperId).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
        }
    }
}
