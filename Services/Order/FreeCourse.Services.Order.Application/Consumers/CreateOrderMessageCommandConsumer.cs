using System.Threading.Tasks;
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.MessageBrokerModels;
using MassTransit;

namespace FreeCourse.Services.Order.Application.Consumers
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var address = new Domain.OrderAggregate.Address(context.Message.Province, context.Message.District, 
                context.Message.Street, context.Message.ZipCode, context.Message.Line);
            Domain.OrderAggregate.Order order = new(context.Message.BuyerId, address);
            context.Message.OrderItems.ForEach(item =>
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.PictureUrl, item.Price);
            });
            await _orderDbContext.Orders.AddAsync(order);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}