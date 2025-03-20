
namespace Domain.Entities
{
    public class Game
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double AverageScore { get; set; }
        public int DeveloperId { get; set; }
        public List<Review> Reviews { get; set; } 
    }
}
