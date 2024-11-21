namespace SelifyApi.Responses;

public class SelifyResponse<T>(T data, string? status = "success", string? message = null)
{
    public T Data { get; set; } = data;
    public string? Status { get; set; } = status;
    public string? Message { get; set; } = message;
}