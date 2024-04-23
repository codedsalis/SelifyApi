using Microsoft.AspNetCore.Mvc;
using SelifyApi.Entities;
using SelifyApi.Requests;

namespace SelifyApi.Interfaces;

public interface IAuthService
{
    public Task<bool> Register(RegistrationRequest request);
    public Task<string> Login(string Email, string Password);
}