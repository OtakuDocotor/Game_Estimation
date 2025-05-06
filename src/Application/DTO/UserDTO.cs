
using Domain.Enums;

namespace Application.DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public UserRoles Role { get; set; }
        public List<ReviewDTO>? Reviews { get; set; }
    }
}
