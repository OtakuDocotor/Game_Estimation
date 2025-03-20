
namespace Application.DTO
{
    public class DeveloperDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GameDTO> Games { get; set; }
        public string LogoURL { get; set; }
    }
}
