using System.Security.Claims;
using TaskSync.Domain.Dtos.Response;
using TaskSync.Domain.Entities;

namespace TaskSync.Infrastructure.Jwt;

public interface IJwtService
{
    Task<JwtToken> GenerateTokenAsync(ApplicationUser user, string expiration = null, IList<Claim> claims = null);
}
