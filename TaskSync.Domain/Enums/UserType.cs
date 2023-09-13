namespace TaskSync.Domain.Enums;

public enum UserType
{
    Admin = 1,
    User
}

public static class UserTypeExtension
{
    public static string? ToStringValue(this UserType userType)
    {
        return userType switch
        {
            UserType.Admin => "Admin",
            UserType.User => "User",
            _ => null,
        };
    }
}
