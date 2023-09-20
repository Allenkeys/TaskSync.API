namespace TaskSync.Infrastructure.CustomExceptions;

public abstract class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {

    }
}
