namespace TaskSync.Domain.Enums;

public enum Priority
{
    Low = 1,
    Medium,
    High
}

public static class PriorityExtension
{
    public static string? ToStringValue(this Priority priority)
    {
        return priority switch
        {
            Priority.Low => "Low",
            Priority.Medium => "Medium",
            Priority.High => "High"
        };
    }
}