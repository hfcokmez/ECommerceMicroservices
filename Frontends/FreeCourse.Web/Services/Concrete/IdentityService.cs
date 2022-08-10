using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Inputs;
using FreeCourse.Web.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace FreeCourse.Web.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ServiceAPISettings _serviceAapiSettings;

        public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,
            IOptions<ClientSettings> clientSettings, IOptions<ServiceAPISettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _clientSettings = clientSettings.Value;
            _serviceAapiSettings = serviceApiSettings.Value;
        }

        public async Task<Response<bool>> SignIn(SignInInput signInInput)
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceAapiSettings.BaseUri,
                Policy = new DiscoveryPolicy()
            });

            if (discovery.IsError) throw discovery.Exception;

            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _clientSettings.WebClientForUser.ClientId,
                ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
                UserName = signInInput.Email,
                Password = signInInput.Password,
                Address = discovery.TokenEndpoint
            };

            var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);
            if (token.IsError)
            {
                var responseContent = await token.HttpResponse.Content.ReadAsStringAsync();
                var errorDTO = JsonSerializer.Deserialize<ErrorDTO>(responseContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return Response<bool>.Fail(errorDTO.Errors, 400);
            }

            var userInfoRequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = discovery.UserInfoEndpoint
            };
            var userInfo = await _httpClient.GetUserInfoAsync(userInfoRequest);

            if (userInfo.IsError) throw userInfo.Exception;

            var claimsIdentity = new ClaimsIdentity(userInfo.Claims,
                CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");
            var principal = new ClaimsPrincipal(claimsIdentity);

            var authenticationProps = new AuthenticationProperties
            {
                IsPersistent = signInInput.IsRemember
            };
            authenticationProps.StoreTokens(new List<AuthenticationToken>
            {
                new() { Name = OpenIdConnectParameterNames.AccessToken, Value = token.AccessToken },
                new() { Name = OpenIdConnectParameterNames.RefreshToken, Value = token.RefreshToken },
                new()
                {
                    Name = OpenIdConnectParameterNames.ExpiresIn,
                    Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString("o", CultureInfo.InvariantCulture)
                }
            });

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal, authenticationProps);

            return Response<bool>.Success(200);
        }

        public Task<TokenResponse> GetAccessTokenByRefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task RevokeRefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}