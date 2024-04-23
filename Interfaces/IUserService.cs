using System.Security.Claims;
using SelifyApi.Entities;

namespace SelifyApi.Interfaces;

public interface IUserService
{
    public Task<User?> GetUser(ClaimsPrincipal claimsPrincipal);
}