using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SelifyApi.Entities;
using SelifyApi.Interfaces;

namespace SelifyApi.Services;

public class UserService(UserManager<User> userManager) : IUserService
{
    public async Task<User?> GetUser(ClaimsPrincipal claimsPrincipal)
    {
        var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "id") ??
                          throw new KeyNotFoundException("Valid ID could not be found in the authenticated token");

        var user = await userManager.FindByIdAsync(userIdClaim!.Value);
        return user;
    }
}