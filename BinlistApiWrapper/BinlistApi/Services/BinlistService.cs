using BinlistApi.Interfaces.Services;
using BinlistApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BinlistApi.Services
{
    public class BinlistService : IBinlistService
    {
        private readonly ApiUrl apiUrl;

        public BinlistService(IOptions<ApiUrl> apiUrl)
        {
            this.apiUrl = apiUrl.Value;
        }

        public async Task<Bin> GetBinDetails(string bin)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri($"{apiUrl.Binlist}/");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response =  await httpClient.GetAsync(bin);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Invalid request - " + response.ReasonPhrase);
            }
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Bin>(resultString);

            return result;
        }
    }
}
