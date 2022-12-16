namespace NotificationHub.Domain.Hubs
{
    public class BasicNotificationHub : INotificationHub
    {
        public async Task SendMessage(Notification notification)
        {
            await Task.FromResult(0);
            Console.WriteLine($"Basic Notification Behavior Called for {notification.ProductID}");
        }
    }
}
