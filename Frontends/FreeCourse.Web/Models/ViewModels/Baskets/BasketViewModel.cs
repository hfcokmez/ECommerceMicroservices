using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeCourse.Web.Models.ViewModels.Baskets
{
    public class BasketViewModel
    {
        public BasketViewModel()
        {
            _basketItems = new List<BasketItemViewModel>();
        }
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        private List<BasketItemViewModel> _basketItems { get; set; }

        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                {
                    _basketItems.ForEach(x =>
                    {
                        var discountPrice = x.Price * ((decimal)DiscountRate.Value / 100);
                        x.ApplyDiscount(Math.Round(x.Price - discountPrice, 2));
                    });
                }

                return _basketItems;
            }
            set { _basketItems = value; }
        }

        public decimal TotalPrice => _basketItems.Sum(x => x.GetCurrentPrice);

        public bool HasDiscount
        {
            get => !string.IsNullOrEmpty(DiscountCode) &&  DiscountRate.HasValue;
        }

        public void CancelDiscount()
        {
            DiscountCode = null;
            DiscountRate = null;
        }
    }
}