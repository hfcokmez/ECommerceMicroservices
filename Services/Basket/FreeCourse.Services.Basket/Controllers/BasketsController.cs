using System.Threading.Tasks;
using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeCourse.Services.Basket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResultInstance(await _basketService.GetBasket(_sharedIdentityService.UserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            basketDto.UserId = _sharedIdentityService.UserId;
            var response = await _basketService.SaveOrUpdate(basketDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return CreateActionResultInstance(await _basketService.Delete(_sharedIdentityService.UserId));
        }
    }
}