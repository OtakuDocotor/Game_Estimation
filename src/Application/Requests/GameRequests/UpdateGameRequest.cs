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
            RuleFor(x => x.ID).NotEmpty().ExclusiveBetween(0,int.MaxValue);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(ValidationConstants.MaxNameLength);
            RuleFor(x => x.AverageScore).ExclusiveBetween(0, int.MaxValue);
            RuleFor(x => x.DeveloperId).NotEmpty().ExclusiveBetween(0, int.MaxValue);
        }
    }
}
