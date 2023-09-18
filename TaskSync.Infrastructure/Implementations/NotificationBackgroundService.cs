using Microsoft.Extensions.Hosting;
using TaskSync.Infrastructure.Interfaces;

namespace TaskSync.Infrastructure.Implementations;

public class NotificationBackgroundService : IHostedService, IDisposable
{
    private readonly INotificationService _notificationService;
    private Timer _timer;
    public NotificationBackgroundService(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
   
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(Notify, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private void Notify(object state)
    {
        _notificationService.CreateStatusNotification();
        _notificationService.CreateDueDateNotification();
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
