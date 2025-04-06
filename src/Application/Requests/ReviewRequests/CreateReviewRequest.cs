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
            RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.MaxNameLength);
            RuleFor(x => x.UserId).NotEmpty().ExclusiveBetween(0, int.MaxValue);
            RuleFor(x => x.GameId).NotEmpty().ExclusiveBetween(0, int.MaxValue);
            RuleFor(x => x.Score).ExclusiveBetween(0, int.MaxValue);
        }
    }
}
