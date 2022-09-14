using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models.ViewModels.Discounts;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrete
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscountViewModel> GetByDiscountCodeAsync(string discountCode)
        {
            var response = await _httpClient.GetAsync($"discounts/{discountCode}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var discount = await response.Content.ReadFromJsonAsync<Response<List<DiscountViewModel>>>();
            return discount.Data.FirstOrDefault();
        }
    }
}