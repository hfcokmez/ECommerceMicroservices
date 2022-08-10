using System;
using System.Collections.Generic;
using System.Linq;
using FreeCourse.Services.Order.Domain.Core;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        private readonly List<OrderItem> _orderItems; //bkz. Backing Field

        public Order()
        {
        }

        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            Address = address;
            BuyerId = buyerId;
        }

        public DateTime CreatedDate { get; }
        public Address Address { get; } //bkz. Shadow Field
        public string BuyerId { get; }

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);

        public void AddOrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (!existProduct) _orderItems.Add(new OrderItem(productId, productName, pictureUrl, price));
        }
    }
}