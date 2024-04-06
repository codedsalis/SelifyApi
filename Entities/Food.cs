namespace SelifyApi.Entities
{
    public class Food
    {
        public Guid Id { get; set;}
        public string Name { get; set;} = default;
        public string Price { get; set;} = default;

        public DateTime CreatedAt { get; set;} = DateTime.Now;

        public DateTime UpdatedAt { get; set;} = DateTime.Now;
    }
}