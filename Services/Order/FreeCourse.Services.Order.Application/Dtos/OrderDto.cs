using System;
using System.Collections.Generic;

namespace FreeCourse.Services.Order.Application.Dtos
{
    public class OrderDto
    {
        private List<OrderItemDto> _orderItems;
        public int Id { get; set; }
        public DateTime CreatedDate { get; private set; }
        public AddressDto Address { get; private set; }
        public string BuyerId { get; private set; }
    }
}