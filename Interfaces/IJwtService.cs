using System.Security.Claims;
using SelifyApi.Entities;

namespace SelifyApi.Interfaces;

public interface IJwtService
{
    public string GenerateToken(User user);

    public ClaimsPrincipal ValidateToken(string token);
}