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
    class RoomService
    {
        private readonly HttpClient _httpClient;

        public RoomService()
        {
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _httpClient = new HttpClient(handler.GetPlatformMessageHandler());
        }

        public async Task<List<Room>> GetRoomAsync()
        {
            var token = await SecureStorage.Default.GetAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"{Config.ApiBaseUrl}/api/Rooms";
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Room>>(response);
        }
        public async Task<List<Room>> GetAllByParcAsync(int id)
        {
            var token = Task.Run(async () => await SecureStorage.Default.GetAsync("access_token")).Result;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"{Config.ApiBaseUrl}/api/Rooms/Parc/{id}";
            var Response = Task.Run(async () => await _httpClient.GetStringAsync(url)).Result;
            return JsonConvert.DeserializeObject<List<Room>>(Response);
        }
    }
}
