using Course.Web.Options;
using Course.Web.Services;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http;

namespace Course.Web.DelegateHandlers
{
    public class ClientAuthenticatedHttpClientHandler(TokenService tokenService , IdentityOption identityOption , IHttpContextAccessor httpContextAccessor): DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (httpContextAccessor.HttpContext is null)
                return await base.SendAsync(request, cancellationToken);

            if (httpContextAccessor.HttpContext.User.Identity!.IsAuthenticated)
                return await base.SendAsync(request, cancellationToken);

            var tokenResponse = await tokenService.GetClientAccessToken();

            if(tokenResponse.IsError)
                throw new UnauthorizedAccessException("Failed to retrieve client access token");

            request.SetBearerToken(tokenResponse.AccessToken!);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
