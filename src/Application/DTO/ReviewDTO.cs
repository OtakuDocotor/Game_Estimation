
namespace Application.DTO
{
    public class ReviewDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int GameId { get; set; }
        public int Score { get; set; }
    }
}
