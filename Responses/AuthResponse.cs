namespace SelifyApi.Responses;

public class AuthResponse
{
    public string Status { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string? Token { get; set; } = null;
}