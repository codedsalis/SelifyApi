using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SelifyApi.Entities
{
    public class Food
    {
        public Guid Id { get; set;}

        public Guid UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Name { get; set;} = string.Empty;

        [Column(TypeName = "varchar(255)")]
        public string Price { get; set;} = string.Empty;

        public DateTime CreatedAt { get; set;} = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set;} = DateTime.UtcNow;
    }
}