namespace NotificationHub.Domain.UserService
{
    public class InMemoryUserService : IUserService
    {
        public async Task<List<string>> GetUserPhonenNumbersSubscribedToProduct(string productId)
        {
            return await Task.FromResult(
                 new List<string>
                 {
                   "+330005002"
                 }
                 );
        }

        public async Task<List<string>> GetUserMailsSubscribedToProduct(string productId)
        {
            return await Task.FromResult(
                 new List<string>
                 {
                    "aym003.hit@gmail.com"
                 }
                 );
        }
    }
}
