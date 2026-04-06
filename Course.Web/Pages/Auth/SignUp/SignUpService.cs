using Course.Web.Options;
using Course.Web.Services;
using Duende.IdentityModel.Client;
using System.Threading;

namespace Course.Web.Pages.Auth.SignUp
{
    public record KeycloakErrorResponse(string ErrorMessage);
    public class SignUpService(IdentityOption identityOption , HttpClient httpClient , ILogger<SignUpService> logger )
    {
        public record UserCreateRequest(string Username , bool Enabled , string FirstName , string LastName , string Email , List<Credential> Credentials);
        public record Credential(string Type , string Value , string Temporary);
        public async Task<ServiceResult> CreateAccount(SignUpViewModel model)
        {
            var token = await GetClientCredentialTokenAsAdmin();
            var address = $"{identityOption.BaseAddress}/admin/realms/CourseTenant/users";

            httpClient.SetBearerToken(token);

            var userCreateRequest = CreateUserCreateRequest(model);
            var response = await httpClient.PostAsJsonAsync(address, userCreateRequest);
            
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.InternalServerError)
                {
                    var keycloakError = await response.Content.ReadFromJsonAsync<KeycloakErrorResponse>();
                    return ServiceResult.Error(keycloakError!.ErrorMessage ?? "An error occurred while creating the user");
                }
                var errorContent = await response.Content.ReadAsStringAsync();
                logger.LogError("User creation failed: {StatusCode}, {ErrorContent}", response.StatusCode, errorContent);
                return ServiceResult.Error("Please try again");
            }

            return ServiceResult.Success();

        }
        private async Task<string> GetClientCredentialTokenAsAdmin()
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

            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Admin.ClientId,
                ClientSecret = identityOption.Admin.ClientSecret,
            });

            if (tokenResponse.IsError)
            {
                throw new Exception($"Token request failed: {tokenResponse.Error}");
            }

            return tokenResponse.AccessToken!;
        }
        private static UserCreateRequest CreateUserCreateRequest(SignUpViewModel model)
        {
            return new UserCreateRequest(
                Username: model.Username,
                Enabled: true,
                FirstName: model.FirstName,
                LastName: model.LastName,
                Email: model.Email,
                Credentials: new List<Credential>
                {
                    new Credential(
                        Type: "password",
                        Value: model.Password,
                        Temporary: "false"
                    )
                }
            );
        }

        
    }
}
