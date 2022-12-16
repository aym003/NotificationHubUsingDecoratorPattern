namespace NotificationHub.Domain.UserService
{
    public interface IUserService
    {
        Task<List<string>> GetUserMailsSubscribedToProduct(string productId);
        Task<List<string>> GetUserPhonenNumbersSubscribedToProduct(string productId);
    }
}
