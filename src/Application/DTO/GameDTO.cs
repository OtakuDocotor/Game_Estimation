
namespace Application.DTO
{
    public class GameDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double AverageScore { get; set; }
        public int DeveloperId { get; set; }
        public List<ReviewDTO> Reviews { get; set; } 
    }
}
