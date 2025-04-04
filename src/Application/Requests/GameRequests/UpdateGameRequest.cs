using FluentValidation;

namespace Application.Requests.GameRequests
{
    public class UpdateGameRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double AverageScore { get; set; }
        public int DeveloperId { get; set; }
    }

    public class UpdateGameRequestValidator : AbstractValidator<UpdateGameRequest>
    {
        public UpdateGameRequestValidator()
        {
            RuleFor(x => x.ID).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("{PropertyName} has 100 maxlength");
            RuleFor(x => x.AverageScore).GreaterThan(0).WithMessage("AVG score must be greater than zero").LessThanOrEqualTo(10).WithMessage("AVG score must be less or equal to 10");
            RuleFor(x => x.DeveloperId).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
        }
    }
}
