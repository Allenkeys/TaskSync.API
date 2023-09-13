using Microsoft.AspNetCore.Identity;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Dtos.Response;
using TaskSync.Infrastructure.Interfaces;

namespace TaskSync.Infrastructure.Implementations;

public class AuthService : IAuthService
{
    public Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> SignUpAsync(CreateUserRequest request)
    {
        throw new NotImplementedException();
    }
}
