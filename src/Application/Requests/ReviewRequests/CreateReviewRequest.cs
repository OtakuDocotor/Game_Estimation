using FluentValidation;

namespace Application.Requests.ReviewRequests
{
    public class CreateReviewRequest
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int GameId { get; set; }
        public int Score { get; set; }
    }

    public class CreateViewRequestValidator : AbstractValidator<CreateReviewRequest>
    {
        public CreateViewRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("{PropertyName} has 100 maxlength");
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
            RuleFor(x => x.GameId).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
            RuleFor(x => x.Score).GreaterThan(0).WithMessage("Score must be greater than zero").LessThanOrEqualTo(10).WithMessage("Score must be less or equal to 10");
        }
    }
}
