using System.ComponentModel.DataAnnotations;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelifyApi.Data;
using SelifyApi.Entities;
using SelifyApi.Interfaces;
using SelifyApi.Requests;

namespace SelifyApi.Services;

public class AuthService(DataContext context, UserManager<User> userManager, IJwtService jwtService, ILogger<AuthService> logger) : IAuthService
{
    private readonly DataContext _context = context;
    private readonly UserManager<User> _userManager = userManager;
    private readonly ILogger<AuthService> _logger = logger;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<string> Login(string Email, string Password)
    {
        var user = await _userManager.FindByEmailAsync(Email) ?? throw new ValidationException("Invalid credentials");

        var isValidPassword = await _userManager.CheckPasswordAsync(user, Password);

        if (!isValidPassword) {
            throw new ValidationException("Invalid credentials");
        }

        string token = _jwtService.GenerateToken(user);
        return token;
    }

    public async Task<bool> Register(RegistrationRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user != null) {
            throw new ValidationException("Email is already taken");
        }

        var newUser = new User
        {
            Email = request.Email,
            UserName = request.Name.ToString().Split(' ')[0],
            NormalizedEmail = request.Email,
            NormalizedUserName = request.Name.ToString().Split(' ')[0],
            Name = request.Name,
            LockoutEnabled = false,
            AccessFailedCount = 0,
            TwoFactorEnabled = false,
            PhoneNumberConfirmed = false,
            EmailConfirmed = false,
            CreatedAt = DateTime.UtcNow,
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);
        // _logger.Log(LogLevel.Debug, "", result);

        if (result.Succeeded)
        {
            // await _userManager.AddToRoleAsync(newUser, role);
            // _jwtService.GenerateToken();
            return true;
        }
        _logger.LogError($"Email: {request.Email}");
        // _logger.LogError($"Error creating user: {string.Join(", ", result.Errors.Select(e => e.Description))}");

        var errorMessages = result.Errors.Select(e => e.Description);
        throw new ValidationException(string.Join(", ", errorMessages));

        // return false;
    }
}