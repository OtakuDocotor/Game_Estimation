
namespace Domain.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public List<Review>? Reviews { get; set; }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}
