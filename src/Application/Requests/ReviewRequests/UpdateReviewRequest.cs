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
            RuleFor(x => x.ID).NotEmpty().ExclusiveBetween(0, int.MaxValue);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.MaxNameLenght).WithMessage("{PropertyName} has 100 maxlength");
            RuleFor(x => x.UserId).NotEmpty().ExclusiveBetween(0, int.MaxValue);
            RuleFor(x => x.GameId).NotEmpty().ExclusiveBetween(0, int.MaxValue);
            RuleFor(x => x.Score).ExclusiveBetween(0, int.MaxValue);
        }
    }
}
