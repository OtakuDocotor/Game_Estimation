
namespace Domain.Entities
{
    public class Review
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; }
        public string? Content { get; set; }
        public int GameId { get; set; }
        public int Score { get; set; }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeUser(int userId)
        {
            UserId = userId;
        }

        public void ChangeContent(string content)
        {
            Content = content;
        }

        public void ChangeGame(int gameId)
        {
            GameId = gameId;
        }   

        public void ChangeScore(int score)
        {
            Score = score;
        }
    }
}
