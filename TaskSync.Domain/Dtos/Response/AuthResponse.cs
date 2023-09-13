namespace TaskSync.Domain.Dtos.Response;

public class AuthResponse
{
    public JwtToken JwtToken { get; set; }
    public string UserId { get; set; }
    public string FullName { get; set; }
    public string UserType { get; set; }
}

public class JwtToken
{
    public string Token { get; set; }
    public DateTime Issued { get; set; } = DateTime.Now;
    public DateTime Expiration { get; set; }
}
