using NotificationHub.Domain.UserService;

namespace NotificationHub.Domain.Hubs.SmsHub
{
    public class SmsDecorator : INotificationHub
    {
        private readonly IUserService _userService;

        private readonly INotificationHub _notificationHub;

        public SmsDecorator(IUserService userService, INotificationHub notificationHub)
        {
            _userService = userService;
            _notificationHub = notificationHub;
        }

        public async Task SendMessage(Notification notification)
        {
            List<string> phoneNumbers = await _userService.GetUserPhonenNumbersSubscribedToProduct(notification.ProductID);

            await _notificationHub.SendMessage(notification);

            await SendMultipleSms(phoneNumbers, notification.ProductID, notification.ProductName);
        }

        private async Task SendMultipleSms(List<string> phoneNumbers, string productID, string productName)
        {
            foreach (string phoneNumber in phoneNumbers)
            {
                await SendSms(phoneNumber, productID, productName);
            }
        }

        private async Task SendSms(string phoneNumber, string productId, string productName)
        {
            await Task.FromResult(0);
            Console.WriteLine($"Sending Sms to {phoneNumber} about product {productId} {productName}");
        }
    }
}
