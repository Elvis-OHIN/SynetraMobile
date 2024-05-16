using SynetraMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;

namespace SynetraMobile.Services
{
    public class ClientService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ClientService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task Login(LoginModel model)
        {
            var httpClient = httpClientFactory.CreateClient("Auth");
            var result = await httpClient.PostAsJsonAsync("/login", model);
            var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
            if (response is not null)
            {
                var serializeResponse = JsonSerializer.Serialize(
                    new LoginResponse() { AccessToken = response.AccessToken, RefreshToken = response.RefreshToken, Email = model.Email });
                await SecureStorage.Default.SetAsync("Authentication", serializeResponse);
            }
        }
    }
}
