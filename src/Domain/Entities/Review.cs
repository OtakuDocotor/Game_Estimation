
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

        public void ChangeName(string name)
        {
            this.Name = name;
        }

        public void ChangeUser(int userId)
        {
            this.UserId = userId;
        }

        public void ChangeContent(string content)
        {
            this.Content = content;
        }

        public void ChangeGame(int gameId)
        {
            this.GameId = gameId;
        }

        public void ChangeScore(int score)
        {
            this.Score = score;
        }
    }
}
