using Microsoft.AspNetCore.Identity;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Dtos.Response;
using TaskSync.Domain.Entities;
using TaskSync.Domain.Enums;
using TaskSync.Infrastructure.Interfaces;
using TaskSync.Infrastructure.Jwt;

namespace TaskSync.Infrastructure.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtService _jwtAuthenticate;
    public AuthService(UserManager<ApplicationUser> userManager, IJwtService jwtAuthenticate, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _jwtAuthenticate = jwtAuthenticate;
        _roleManager = roleManager;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);
        bool isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValidPassword || user is null)
            throw new InvalidOperationException("Invalid email or password");

        if (!user.Active)
            throw new InvalidOperationException("Account is inactive");

        string userType = user.UserTypeId.ToStringValue()!;

        string fullname = string.IsNullOrWhiteSpace(user.Middlename)
            ? $"{user.Firstname} {user.Lastname}"
            : $"{user.Firstname} {user.Middlename} {user.Lastname}";

        JwtToken token = await _jwtAuthenticate.GenerateTokenAsync(user);

        return new AuthResponse
        {
            JwtToken = token,
            UserId = user.Id,
            FullName = fullname,
            UserType = userType,
        };
    }

    public async Task<IdentityResult> SignUpAsync(CreateUserRequest request)
    {
        ApplicationUser existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            throw new InvalidOperationException($"User with email: {request.Email} already exist");

        ApplicationUser user = new()
        {
            Firstname = request.Firstname,
            Middlename = request.Middlename,
            UserName = request.Username,
            Lastname = request.Lastname,
            Email = request.Email,
            UserTypeId = request.UserTypeId,
        };

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new InvalidOperationException($"Failed to create user: {result.Errors.FirstOrDefault()!.Description}");

        bool roleExists = await _roleManager.RoleExistsAsync(request.UserTypeId.ToStringValue());

        if (!roleExists)
        {
            await _userManager.AddToRoleAsync(user, UserType.User.ToStringValue());
            return result;
        }

        await _userManager.AddToRoleAsync(user, request.UserTypeId.ToStringValue());

        return result;
    }
}
