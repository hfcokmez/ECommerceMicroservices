using System.Collections.Generic;

namespace FreeCourse.Web.Models.Inputs.Orders
{
    public class OrderCreateInput
    {
        public OrderCreateInput()
        {
            OrderItems = new List<OrderItemCreateInput>();
        }
        public string BuyerId { get; set; }
        public List<OrderItemCreateInput> OrderItems { get; set; }
        public AddressCreateInput Address { get; set; }
    }
}