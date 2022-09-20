using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.Inputs.FakePayment;
using FreeCourse.Web.Models.Inputs.Orders;
using FreeCourse.Web.Models.ViewModels.Orders;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _httpClient = httpClient;
            _paymentService = paymentService;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckOutInfoInput checkOutInfoInput)
        {
            var basket = await _basketService.Get();
            var payment = new PaymentInfoInput()
            {
                CardName = checkOutInfoInput.CardName,
                CardNumber = checkOutInfoInput.CardNumber,
                CVV = checkOutInfoInput.CVV,
                Expiration = checkOutInfoInput.Expiration,
                TotalPrice = basket.TotalPrice
            };
            var responsePayment = await _paymentService.ReceivePayment(payment);
            if (!responsePayment)
            {
                return new OrderCreatedViewModel() { Error = "Payment failed", IsSuccessful = false };
            }

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.UserId,
                Address = new AddressCreateInput()
                {
                    Province = checkOutInfoInput.Province,
                    District = checkOutInfoInput.District,
                    Line = checkOutInfoInput.Line,
                    Street = checkOutInfoInput.Street,
                    ZipCode = checkOutInfoInput.ZipCode
                }
            };
            
            foreach (var basketItem in basket.BasketItems)
            {
                var orderItemCreate = new OrderItemCreateInput()
                {
                    ProductId = basketItem.CourseId,
                    ProductName = basketItem.CourseName,
                    Price = basketItem.GetCurrentPrice,
                    PictureUrl = ""
                };
                orderCreateInput.OrderItems.Add(orderItemCreate);
            }

            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);
            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel() { Error = "Order cannot be created", IsSuccessful = false };
            }

            var orderCreated = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
            orderCreated.Data.IsSuccessful = true;
            await _basketService.Delete();
            return orderCreated.Data;
        }

        public Task SuspendOrder(CheckOutInfoInput checkOutInfoInput)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<OrderViewModel>> GetOrders()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response.Data;
        }
    }
}