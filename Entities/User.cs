using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SelifyApi.Entities;

public class User : IdentityUser<Guid>
{
    public new Guid Id { get; set;}

    [Column(TypeName = "varchar(255)")]
    public string Name { get; set;} = string.Empty;

    [Column(TypeName = "varchar(255)")]
    public override string? Email { get; set;} = string.Empty;

    public DateTime CreatedAt { get; set;} = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set;} = DateTime.UtcNow;
}