using BinlistApi.Interfaces.Managers;
using BinlistApi.Interfaces.Services;
using BinlistApi.Models;
using BinlistApi.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BinlistApi.Managers
{
    public class BinlistManager : IBinlistManager
    {
        private readonly IBinApiService binApiService;

        public BinlistManager(IBinApiService binApiService)
        {
            this.binApiService = binApiService;
        }

        public async Task<Bin> GetBinDetails(string bin)
        {
            var binDetails = await binApiService.GetBinDetails(bin);

            return binDetails;
        }
        
    }
}
