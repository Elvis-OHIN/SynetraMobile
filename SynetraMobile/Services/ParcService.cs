using Newtonsoft.Json;
using SynetraUtils.Models.DataManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SynetraMobile.Services
{
    class ParcService
    {
        private readonly HttpClient _httpClient;

        public ParcService()
        {
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler());
        }

        public async Task<List<Parc>> GetParcAsync()
        {
            var token = await SecureStorage.Default.GetAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"{Config.ApiBaseUrl}/api/Parcs";
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Parc>>(response);
        }

    }
}
