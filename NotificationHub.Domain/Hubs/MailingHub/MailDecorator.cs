using NotificationHub.Domain.UserService;

namespace NotificationHub.Domain.Hubs.MailingHub
{
    public class MailDecorator : INotificationHub
    {
        private readonly IUserService _userService;
        private readonly INotificationHub _notificationHub;

        public MailDecorator(IUserService userService, INotificationHub notificationHub)
        {
            _userService = userService;
            _notificationHub = notificationHub;
        }

        public async Task SendMessage(Notification notification)
        {
            List<string> mailsList = await _userService.GetUserMailsSubscribedToProduct(notification.ProductID);

            await _notificationHub.SendMessage(notification);

            await SendMultipleEmailAsync(mailsList, notification);
        }

        private async Task SendMultipleEmailAsync(List<string> mailsList, Notification notification)
        {

            foreach (string mailAdress in mailsList)
            {
                await SendEmailAsync(notification, mailAdress);
            }
        }

        private async Task SendEmailAsync(Notification notification, string mailAdress)
        {
            await Task.FromResult(0);
            Console.WriteLine($"Mail sent for product {notification.ProductID} to user {mailAdress}");
        }

    }
}
