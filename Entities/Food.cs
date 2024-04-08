namespace SelifyApi.Entities
{
    public class Food
    {
        public Guid Id { get; set;}
        public string Name { get; set;} = string.Empty;
        public string Price { get; set;} = string.Empty;

        public DateTime CreatedAt { get; set;} = DateTime.Now;

        public DateTime UpdatedAt { get; set;} = DateTime.Now;
    }
}