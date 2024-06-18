using Newtonsoft.Json;
using SynetraUtils.Models.DataManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SynetraMobile.Services
{
    class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService()
        {
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler());
        }

        public async Task<User> GetMe()
        {
            var token = await SecureStorage.Default.GetAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"{Config.ApiBaseUrl}/api/Users/Me";
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<User>(response);
        }

    }
}
