
namespace Application.DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<ReviewDTO> Reviews { get; set; }
    }
}
