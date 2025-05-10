
using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public UserRoles Role { get; set; }
        public List<Review>? Reviews { get; set; }
        public int? LogoAttachmentId { get; set; }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}
