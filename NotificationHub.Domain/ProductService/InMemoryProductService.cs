using Microsoft.AspNetCore.SignalR;
using NotificationHub.Domain.Hubs;
using NotificationHub.Domain.Hubs.MailingHub;
using NotificationHub.Domain.Hubs.SignalRHub;
using NotificationHub.Domain.Hubs.SmsHub;
using NotificationHub.Domain.Models;
using NotificationHub.Domain.UserService;

namespace NotificationHub.Domain.ProductService
{
    public class InMemoryProductService : IProductService
    {

        private readonly List<Product> _products;
        private IServiceProvider _serviceProvider;

        public InMemoryProductService(IServiceProvider serviceProvider)
        {
            _products = new List<Product>
            {
                new Product
                {
                    ProductId="P01",
                    ProductName="Cool Product",
                    Description="World coolest Product",
                    Price=9.99m,
                    Stock=10,
                    OnSale=false
                },
                new Product
                {
                    ProductId="P02",
                    ProductName="Cool Expensive Product",
                    Description="World coolest expensive Product",
                    Price=999.99m,
                    Stock=0,
                    OnSale=false
                }
            };

            _serviceProvider = serviceProvider;
        }

        public Task<List<Product>> GetProducts()
        {
            return Task.FromResult(_products);
        }

        public async Task UpdateProduct(Product product)
        {
            var foundProduct = _products.FirstOrDefault(x => x.ProductId == product.ProductId);

            if (foundProduct != null)
            {
                INotificationHub notificationHub = GetNotificationHubs(product, foundProduct);
                await notificationHub.SendMessage(new Notification
                {
                    ProductID = product.ProductId,
                    ProductName = product.ProductName,
                    Message = "i did it"
                });
            }
        }

        private INotificationHub GetNotificationHubs(Product product, Product foundProduct)
        {
            INotificationHub notificationHub = new BasicNotificationHub();

            if (product.Stock > foundProduct.Stock)
            {
                notificationHub = new SignalRDecorator((IHubContext<ProductNotificationHub, INotificationHub>)_serviceProvider.GetService(typeof(IHubContext<ProductNotificationHub, INotificationHub>)), notificationHub);
            }

            if (product.Price != foundProduct.Price)
            {
                notificationHub = new MailDecorator((IUserService)_serviceProvider.GetService(typeof(IUserService)), notificationHub);
            }

            if (product.OnSale != foundProduct.OnSale)
            {
                notificationHub = new SmsDecorator((IUserService)_serviceProvider.GetService(typeof(IUserService)), notificationHub);
            }

            return notificationHub;
        }
    }
}
