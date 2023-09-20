namespace TaskSync.Infrastructure.CustomExceptions;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException() : base("User not found!")
    {
        
    }
    public UserNotFoundException(string message) : base(message)
    {
        
    }
    public UserNotFoundException(object id) : base($"User with Id: {id}, not found in our servers")
    {
        
    }
}
