using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskSync.Domain.Dtos.Response;
using TaskSync.Domain.Entities;
using TaskSync.Domain.Enums;

namespace TaskSync.Infrastructure.Jwt;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<JwtToken> GenerateTokenAsync(ApplicationUser user, string expiration = null, IList<Claim> claims = null)
    {
        var signingCredentials = await GetSigningCredentialsAsync();

        var allClaims = await GetClaimsAsync(user, claims);

        var securityToken = await GenerateSecurityTokenAsync(signingCredentials, allClaims);

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

        var jwtSettings = _configuration.GetSection("JwtConfig");

        return new JwtToken()
        {
            Token = jwtToken,
            Expiration = DateTime.Now.AddHours(double.Parse(jwtSettings["Expires"]))
        };
    }

    private async Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, IList<Claim> claims = null)
    {
        IdentityOptions options = new();
        var claimsBuilder = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id),
                new Claim(options.ClaimsIdentity.UserNameClaimType, user.UserName),
                new Claim(ClaimTypes.Role, user.UserTypeId.ToStringValue())
            };
        if (claims != null)
            claimsBuilder.AddRange(claims);

        return claimsBuilder;
    }

    private async Task<SigningCredentials> GetSigningCredentialsAsync()
    {
        var jwtSettings = _configuration.GetSection("JwtConfig");
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["key"]));
        return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
    }

    private async Task<JwtSecurityToken> GenerateSecurityTokenAsync(SigningCredentials credentials, IList<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtConfig");

        return new JwtSecurityToken
            (
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(double.Parse(jwtSettings["Expires"])),
                signingCredentials: credentials
            );
    }
}
