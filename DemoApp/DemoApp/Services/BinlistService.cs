using DemoApp.Interfaces.Managers;
using DemoApp.Interfaces.Services;
using DemoApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DemoApp.Services
{
    public class BinlistService : IBinlistService
    {
        private readonly ApiUrl apiUrl;
        private readonly ITokenManager tokenManager;
        private readonly ILogger<BinlistService> logger;

        public BinlistService(IOptions<ApiUrl> apiUrl, ITokenManager tokenManager, ILogger<BinlistService> logger)
        {
            this.apiUrl = apiUrl.Value;
            this.tokenManager = tokenManager;
            this.logger = logger;
        }

        public async Task<Bin> GetBinDetails(string bin,int count=0)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri($"{apiUrl.Binlist}/api/binlist/");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var token = await tokenManager.GetToken();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(bin);

            if (!response.IsSuccessStatusCode)
            {
                
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var newtoken = await tokenManager.RequestNewToken();

                    if (!string.IsNullOrEmpty(newtoken) && count == 0)
                    {
                        count++;

                        await GetBinDetails(bin,count);
                    }
                    
                }
                logger.LogError($"Invalid request - {response.ReasonPhrase}");
                throw new Exception("Invalid request - " + response.ReasonPhrase);
            }
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Bin>(resultString);

            return result;
        }
        
    }
}
