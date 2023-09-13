using Microsoft.AspNetCore.Identity;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Dtos.Response;

namespace TaskSync.Infrastructure.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> SignUpAsync(CreateUserRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}
