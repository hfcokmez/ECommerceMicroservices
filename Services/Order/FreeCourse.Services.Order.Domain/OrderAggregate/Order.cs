using System;
using System.Collections.Generic;
using System.Linq;
using FreeCourse.Services.Order.Domain.Core;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
	public class Order : Entity, IAggregateRoot
	{
        public DateTime CreatedTime { get; private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }
        private readonly List<OrderItem> _orderItems;

        public Order(Address address, string buyerId)
        {
            Address = address;
            BuyerId = buyerId;
            CreatedTime = DateTime.Now;
            _orderItems = new List<OrderItem>();
        }

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var productExist = _orderItems.Any(x => x.ProductId == productId);
            if (!productExist)
            {
                var orderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(orderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}

