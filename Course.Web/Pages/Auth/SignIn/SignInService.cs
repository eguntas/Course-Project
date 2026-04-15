using Course.Web.Options;
using Course.Web.Pages.Auth.SignUp;
using Course.Web.Services;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Course.Web.Pages.Auth.SignIn
{
    public class SignInService(IHttpContextAccessor contextAccessor , TokenService tokenService, IdentityOption identityOption, HttpClient httpClient, ILogger<SignInService> logger)
    {
        public async Task<ServiceResult> SignInAsync(SignInViewModel model)
        {
            try
            {
                var tokenResponse = await GetAccessTokenAsAdmin(model);
                if (tokenResponse.IsError)
                {
                    logger.LogError("Token request failed: {Error}", tokenResponse.Error);
                    return ServiceResult.Error(tokenResponse.Error! , tokenResponse.ErrorDescription!);
                }
                
                var userClaims = tokenService.ExtractClaims(tokenResponse.AccessToken!);
                var authenticationProperties = tokenService.CreateAuthenticationProperties(tokenResponse);
                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme,ClaimTypes.Name,ClaimTypes.Role);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await contextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Sign-in failed");
                return ServiceResult.Error("Sign-in failed", ex.Message);
            }
        }

        private async Task<TokenResponse> GetAccessTokenAsAdmin(SignInViewModel model)
        {
            var discoveryRequest = new DiscoveryDocumentRequest
            {
                Address = identityOption.Address,
                Policy = { RequireHttps = false }
            };

            httpClient.BaseAddress = new Uri(identityOption.Address);
            var discoveryResponse = await httpClient.GetDiscoveryDocumentAsync(discoveryRequest);

            if (discoveryResponse.IsError)
            {
                throw new Exception($"Discovery document retrieval failed: {discoveryResponse.Error}");
            }

            var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret,
                UserName = model.Email,
                Password = model.Password,
                Scope = "offline_access"
            });

            
            return tokenResponse;
        }
    }
}





   
