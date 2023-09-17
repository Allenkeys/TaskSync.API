using Microsoft.Extensions.Hosting;

namespace TaskSync.Infrastructure.Implementations;

public class NotificationBackgroundService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}
