using Course.Web.Options;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Course.Web.Services
{
    public class TokenService(HttpClient httpClient , IdentityOption identityOption)
    {
        public List<Claim> ExtractClaims(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);
            return jwtToken.Claims.ToList();
        }

        public AuthenticationProperties CreateAuthenticationProperties(TokenResponse tokenResponse)
        {
            var authenticationTokens = new List<AuthenticationToken>
            {
                new()
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = tokenResponse.AccessToken!
                },
                new()
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = tokenResponse.RefreshToken!
                },
                new()
                {
                    Name = OpenIdConnectParameterNames.ExpiresIn,
                    Value = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn).ToString("o") // ISO 8601 format
                }
            };
            AuthenticationProperties authenticationProperties = new AuthenticationProperties()
            {
                IsPersistent = true,
            };
           
            authenticationProperties.StoreTokens(authenticationTokens);

            return authenticationProperties;
        }

        public async Task<TokenResponse> GetTokenByRefreshToken(string refreshToken)
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

            var tokenResponse = await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret,
                RefreshToken = refreshToken
            });

            return tokenResponse;

        }

        public async Task<TokenResponse> GetClientAccessToken()
        {
            var discoveryRequest = new DiscoveryDocumentRequest
            {
                Address = identityOption.Address,
                Policy = { RequireHttps = false }
            };

            httpClient.BaseAddress = new Uri(identityOption.Address);
            var discoveryResponse = await httpClient.GetDiscoveryDocumentAsync();

            if (discoveryResponse.IsError)
            {
                throw new Exception($"Discovery document retrieval failed: {discoveryResponse.Error}");
            }

            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret
            });

            if(tokenResponse.IsError)
                throw new Exception($"Token request failed: {tokenResponse.Error}");

            return tokenResponse;
        }
    }
}
