using BinlistApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BinlistApi.Services
{
    public interface IBinApiService
    {
        Task<Bin> GetBinDetails(string bin);
    }

    public class BinApiService : IBinApiService
    {
        private readonly ApiUrl apiUrl;
        private readonly HttpClient httpClient;

        public BinApiService(IOptions<ApiUrl> apiUrl, HttpClient httpClient)
        {
            this.apiUrl = apiUrl.Value;
            this.httpClient = httpClient;
        }

        public async Task<Bin> GetBinDetails(string bin)
        {
            var response = await httpClient.GetAsync(bin);

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
