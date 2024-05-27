using SynetraMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using IdentityModel.OidcClient;

namespace SynetraMobile.Services
{
    public class ClientService
    {
        private HttpClient httpClient;
        

        public async Task<bool> Login(LoginModel model)
        {
           
#if DEBUG
                HttpsClientHandlerService handler = new HttpsClientHandlerService();
                httpClient = new HttpClient(handler.GetPlatformMessageHandler());
#else
                httpClient = new HttpClient();
#endif
                string url = "https://10.0.2.2:7082";
                httpClient.BaseAddress = new Uri(url);
                var result = await httpClient.PostAsJsonAsync("login", model);

                if (result.IsSuccessStatusCode)
                {
                    var jsonResponse = await result.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<JwtToken>(jsonResponse);
                    await SecureStorage.Default.SetAsync("access_token", token.AccessToken);
                    if (token.RefreshToken != null)
                    {
                        await SecureStorage.Default.SetAsync("refresh_token", token.RefreshToken);
                    }
                    return true;
                }

                return false;
            
        }
    }
}
