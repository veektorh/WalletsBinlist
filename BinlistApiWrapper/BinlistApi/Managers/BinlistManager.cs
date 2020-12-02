using BinlistApi.Interfaces.Managers;
using BinlistApi.Interfaces.Services;
using BinlistApi.Models;
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
        private readonly IBinlistService binlistService;

        public BinlistManager(IBinlistService binlistService)
        {
            this.binlistService = binlistService;
        }

        public async Task<Bin> GetBinDetails(string bin)
        {
            var binDetails = await binlistService.GetBinDetails(bin);

            return binDetails;
        }
        
    }
}
