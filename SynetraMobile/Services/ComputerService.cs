using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SynetraUtils.Models.DataManagement;

namespace SynetraMobile.Services
{
    class ComputerService
    {
        private readonly HttpClient _httpClient;

        public ComputerService()
        {
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler());
        }

        public async Task<List<Computer>> GetComputersAsync()
        {
            var token = Task.Run(async () => await SecureStorage.Default.GetAsync("access_token")).Result;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"{Config.ApiBaseUrl}/api/Computers";
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Computer>>(response);
        }

        public async Task<List<Computer>> GetAllByParcAsync(int id)
        {
            var token = Task.Run(async () => await SecureStorage.Default.GetAsync("access_token")).Result;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"{Config.ApiBaseUrl}/api/Computers/Parc/{id}";
            var Response = Task.Run(async () => await _httpClient.GetStringAsync(url)).Result;
            return JsonConvert.DeserializeObject<List<Computer>>(Response); 
        }
    }
}
