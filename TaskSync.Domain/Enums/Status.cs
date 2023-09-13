namespace TaskSync.Domain.Enums;

public enum Status
{
    Pending = 1,
    Inprogress,
    Completed
}

public static class StatusExtension
{
    public static string? ToStringValue(this Status status)
    {
        return status switch
        {
            Status.Pending => "Pending",
            Status.Inprogress => "Inprogress",
            Status.Completed => "Completed"
        };
    }
}
