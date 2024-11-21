namespace SelifyApi.Responses;

public class AuthResponse
{
    public string Status { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public Tokens? Tokens{ get; set; }
}

public class Tokens 
{
    public string AccessToken {get; set;} = string.Empty;
    public string? RefreshToken {get; set;} = string.Empty;
}