
namespace Domain.Entities
{
    public class Game
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public double AverageScore { get; set; }
        public int DeveloperId { get; set; }
        public List<Review>? Reviews { get; set; }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeAvgScore(double avgScore)
        {
            AverageScore = avgScore;
        }

        public void ChangeDeveloper(int id)
        {
            DeveloperId = id;
        }   
    }
}
