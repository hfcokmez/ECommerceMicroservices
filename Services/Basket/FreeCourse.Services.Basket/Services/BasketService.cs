using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDatabase().KeyDeleteAsync(userId);
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found", 404);
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            try
            {
                var basket = await _redisService.GetDatabase().StringGetAsync(userId);
                if (string.IsNullOrEmpty(basket))
                {
                    var emptyBasket = new BasketDto()
                    {
                        UserId = userId,
                        BasketItems = new List<BasketItemDto>()
                    };
                    var result = await SaveOrUpdate(emptyBasket);
                    if (!result.IsSuccessful)
                    {
                        return Response<BasketDto>.Fail("The basket not found", 404);
                    }
                    basket = JsonSerializer.Serialize(emptyBasket);
                }
                return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(basket), 200);
            }
            catch (Exception)
            {
                return Response<BasketDto>.Fail("The basket not found", 500);
            }
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDatabase()
                .StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket could not updated or saved", 500);
        }
    }
}