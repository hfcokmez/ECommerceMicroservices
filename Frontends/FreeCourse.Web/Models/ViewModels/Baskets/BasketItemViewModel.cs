namespace FreeCourse.Web.Models.ViewModels.Baskets
{
    public class BasketItemViewModel
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;
        private decimal? DiscountAppliedPrice { get; set; }
        public decimal GetCurrentPrice
        {
            get => DiscountAppliedPrice != null ? DiscountAppliedPrice.Value : Price;
        }
        public void ApplyDiscount(decimal discountPrice)
        {
            DiscountAppliedPrice = discountPrice;
        }
    }
}