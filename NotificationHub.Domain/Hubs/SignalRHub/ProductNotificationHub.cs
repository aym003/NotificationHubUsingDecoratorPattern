using Microsoft.AspNetCore.SignalR;

namespace NotificationHub.Domain.Hubs.SignalRHub
{
    public class ProductNotificationHub : Hub<INotificationHub>
    {
        public Task SuscribeToProduct(string productId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, productId);
        }
    }
}
