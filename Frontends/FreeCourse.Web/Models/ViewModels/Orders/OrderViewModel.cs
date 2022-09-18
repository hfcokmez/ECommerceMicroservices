using System;
using System.Collections.Generic;

namespace FreeCourse.Web.Models.ViewModels.Orders
{
    public class OrderViewModel
    {
        private List<OrderItemViewModel> _orderItems;
        public int Id { get; set; }
        public DateTime CreatedDate { get; private set; }
        // There is no need to get the Address of the order rn.
        // public AddressViewModel Address { get; private set; }
        public string BuyerId { get; private set; }
    }
}