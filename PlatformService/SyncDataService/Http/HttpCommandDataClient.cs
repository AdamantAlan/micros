using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformService.SyncDataService.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PostAsync($"{_config["CommandService"]}/api/com/Platforms", httpContent);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Sync post to CommandService was OK");
            }
            else
            {
                Console.WriteLine($"Sync post to CommandService was not OK\n{(int)response.StatusCode}");
            }
        }
    }
}
