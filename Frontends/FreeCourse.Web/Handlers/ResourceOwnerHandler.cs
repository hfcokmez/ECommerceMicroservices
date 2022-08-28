using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreeCourse.Web.Exceptions;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace FreeCourse.Web.Handlers
{
    public class ResourceOwnerHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IIdentityService _identityService;
        private readonly ILogger<ResourceOwnerHandler> _logger;

        public ResourceOwnerHandler(IHttpContextAccessor contextAccessor, IIdentityService identityService, ILogger<ResourceOwnerHandler> logger)
        {
            _contextAccessor = contextAccessor;
            _identityService = identityService;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var tokenResponse = await _identityService.GetAccessTokenByRefreshToken();
                if (tokenResponse != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
                    response = await base.SendAsync(request, cancellationToken);
                }
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnAuthorizedException();
            }
            
            return response;
        }
    }
}