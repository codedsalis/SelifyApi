using System.ComponentModel.DataAnnotations;

namespace SelifyApi.Requests;

public class UpdateFoodRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Price { get; set; } = string.Empty;
}