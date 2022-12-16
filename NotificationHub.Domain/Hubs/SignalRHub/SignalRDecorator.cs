using Microsoft.AspNetCore.SignalR;

namespace NotificationHub.Domain.Hubs.SignalRHub
{
    public class SignalRDecorator : INotificationHub
    {
        private readonly IHubContext<ProductNotificationHub, INotificationHub> _signalrNotificator;

        private readonly INotificationHub _notificationHub;

        public SignalRDecorator(IHubContext<ProductNotificationHub, INotificationHub> hubContext, INotificationHub notificationHub)
        {
            _signalrNotificator = hubContext;
            _notificationHub = notificationHub;
        }


        public async Task SendMessage(Notification notification)
        {
            await _notificationHub.SendMessage(notification);

            await _signalrNotificator.Clients.Group(notification.ProductID).SendMessage(notification);
        }
    }
}
