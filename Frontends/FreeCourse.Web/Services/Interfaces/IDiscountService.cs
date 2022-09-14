using System.Threading.Tasks;
using FreeCourse.Web.Models.ViewModels.Discounts;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetByDiscountCodeAsync(string discountCode);
    }
}