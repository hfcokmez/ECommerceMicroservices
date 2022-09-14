using System;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Handlers;
using FreeCourse.Web.Helpers;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Concrete;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreeCourse.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void LoadHttpClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var serviceAPISettings = Configuration.GetSection("ServiceAPISettings").Get<ServiceAPISettings>();
            services.AddHttpClient<IIdentityService, IdentityService>();
            services.AddHttpClient<IUserService, UserService>(opt =>
            { 
                opt.BaseAddress = new Uri(serviceAPISettings.IdentityBaseUri);
            }).AddHttpMessageHandler<ResourceOwnerHandler>();
            services.AddHttpClient<ICatalogService, CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceAPISettings.GatewayBaseUri}/{serviceAPISettings.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();
            services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceAPISettings.GatewayBaseUri}/{serviceAPISettings.PhotoStock.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();
            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
            services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceAPISettings.GatewayBaseUri}/{serviceAPISettings.Basket.Path}");
            }).AddHttpMessageHandler<ResourceOwnerHandler>();
            services.AddHttpClient<IDiscountService, DiscountService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceAPISettings.GatewayBaseUri}/{serviceAPISettings.Discount.Path}");
            }).AddHttpMessageHandler<ResourceOwnerHandler>();
        }

        public static void LoadServices(this IServiceCollection services)
        {
            services.AddScoped<ResourceOwnerHandler>();
            services.AddScoped<ClientCredentialTokenHandler>();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddSingleton<PhotoHelper>();
        }
    }
}