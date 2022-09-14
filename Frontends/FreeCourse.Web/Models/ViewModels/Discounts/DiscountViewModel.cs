using System;

namespace FreeCourse.Web.Models.ViewModels.Discounts
{
    public class DiscountViewModel
    {
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}