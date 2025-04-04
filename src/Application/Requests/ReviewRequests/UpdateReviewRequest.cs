using FluentValidation;

namespace Application.Requests.ReviewRequests
{
    public class UpdateReviewRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int GameId { get; set; }
        public int Score { get; set; }
    }

    public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
    {
        public UpdateReviewRequestValidator()
        {
            RuleFor(x => x.ID).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("{PropertyName} has 100 maxlength");
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
            RuleFor(x => x.GameId).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
            RuleFor(x => x.Score).GreaterThan(0).WithMessage("Score must be greater than zero").LessThanOrEqualTo(10).WithMessage("Score must be less or equal to 10");
        }
    }
}
