using Microsoft.Extensions.Hosting;
using TaskSync.Infrastructure.Interfaces;

namespace TaskSync.Infrastructure.Implementations;

public class NotificationBackgroundService : IHostedService, IDisposable
{
    private readonly INotificationService _notificationService;
    private Timer _statusTimer;
    private Timer _dateTimer;
    public NotificationBackgroundService(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
   
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _dateTimer = new Timer(DueDateNotify, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        _statusTimer = new Timer(StatusNotify, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _statusTimer?.Change(Timeout.Infinite, 0);
        _dateTimer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private void StatusNotify(object state)
    {
        _notificationService.CreateStatusNotification();
    }

    private void DueDateNotify(object state)
    {
        _notificationService.CreateDueDateNotification();
    }

    public void Dispose()
    {
        _dateTimer?.Dispose();
        _statusTimer?.Dispose();
    }
}
