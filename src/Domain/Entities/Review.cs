
namespace Domain.Entities
{
    public class Review
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int GameId { get; set; }
        public int Score { get; set; }
    }
}
