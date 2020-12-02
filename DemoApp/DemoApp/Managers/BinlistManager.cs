using DemoApp.Interfaces.Managers;
using DemoApp.Interfaces.Services;
using DemoApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Managers
{
    public class BinlistManager : IBinlistManager
    {
        private readonly IBinlistService binlistService;
        private readonly AppDbContext appDbContext;
        private readonly ICacheManager cacheManager;
        private readonly ILogger<BinlistManager> logger;

        public BinlistManager(IBinlistService binlistService, AppDbContext appDbContext, 
            ICacheManager cacheManager,
            ILogger<BinlistManager> logger)
        {
            this.binlistService = binlistService;
            this.appDbContext = appDbContext;
            this.cacheManager = cacheManager;
            this.logger = logger;
        }

        public async Task<Bin> GetBinDetails(string bin)
        {
            logger.LogInformation($"Getting bin details for {bin}");

            //get bin details from cache if it exists
            var binFromCache = cacheManager.Get(bin);

            Bin binDetails = new Bin();

            if (!string.IsNullOrEmpty(binFromCache))
            {
                binDetails = JsonConvert.DeserializeObject<Bin>(binFromCache);
            }
            else
            {
                binDetails = await binlistService.GetBinDetails(bin);

                var binDetailsString = JsonConvert.SerializeObject(binDetails);

                cacheManager.Set(bin, binDetailsString);
            }
            

            await IncrementCount(bin);

            logger.LogInformation($"bin details response {JsonConvert.SerializeObject(binDetails)}");

            return binDetails;
        }

        public async Task<BinCountModel> GetCount()
        {
            var bins = await appDbContext.BinCounts.ToListAsync();
            var size = bins.Select(a => a.Count).Sum();

            return new BinCountModel {
                success = true,
                size = size,
                response = bins.ToDictionary(a => a.Bin, a => a.Count)
            };
        }

        public async Task<long> IncrementCount(string bin)
        {
            var bindetails = await appDbContext.BinCounts.FirstOrDefaultAsync(a => a.Bin == bin);

            if (bindetails == null)
            {
                await appDbContext.BinCounts.AddAsync(new BinCount { Bin = bin, Count = 1 });
                await appDbContext.SaveChangesAsync();
                return 1;
            }

            bindetails.Count = bindetails.Count + 1;

            await appDbContext.SaveChangesAsync();

            return bindetails.Count;
        }
    }

}
