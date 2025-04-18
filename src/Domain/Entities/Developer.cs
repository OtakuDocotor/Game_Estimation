
namespace Domain.Entities
{
    public class Developer
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Game>? Games { get; set; }
        public string? LogoURL { get; set; }

        public void ChangeName(string? name)
        {
            Name = name;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }

        public void ChangeLogo(string logo)
        {
            LogoURL = logo;
        }
    }
}
