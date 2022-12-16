namespace NotificationHub.Domain.Hubs.NoticationDecorator
{
    public abstract class NotifiationDecorator : INotificationHub
    {
        protected INotificationHub _notificationHub;
        public NotifiationDecorator(INotificationHub notificationHub)
        {
            _notificationHub = notificationHub;
        }

        public virtual Task SendMessage(Notification notification)
        {
            return _notificationHub.SendMessage(notification);
        }
    }
}

