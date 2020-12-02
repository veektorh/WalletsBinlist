using DemoApp.Interfaces.Managers;
using DemoApp.Interfaces.Services;
using DemoApp.Managers;
using DemoApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Logging;

namespace DemoAppTest
{
    [TestClass]
    public class BinlistManagerTest
    {
        public BinlistManagerTest()
        {

        }


        [TestMethod]
        public async Task IncrementCountTest()
        {
            var id = "1234";
            var binDetailsResult = new Bin() { id = id };

            var mockBinlistService = new Mock<IBinlistService>();
            var mockCacheManager = new Mock<ICacheManager>();
            var mockLogger = new Mock<ILogger<BinlistManager>>();

            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var mockAppContext = new AppDbContext(dbContextOptions);
            

            var binlistManager = new BinlistManager(mockBinlistService.Object, mockAppContext, mockCacheManager.Object, mockLogger.Object);

            //Add 1 record
            var result = await binlistManager.IncrementCount(id) ;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);


            //Increment db records by 3
            long add_more_result = 0;
            for (int i = 0; i < 3; i++)
            {
                add_more_result = await binlistManager.IncrementCount(id);
            }

            Assert.AreEqual(4, add_more_result);

        }

        [TestMethod]
        public async Task GetCountTest()
        {
            var id = "1234";
            var binDetailsResult = new Bin() { id = id };

            var mockBinlistService = new Mock<IBinlistService>();
            var mockCacheManager = new Mock<ICacheManager>();
            var mockLogger = new Mock<ILogger<BinlistManager>>();

            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var mockAppContext = new AppDbContext(dbContextOptions);

            var binlistManager = new BinlistManager(mockBinlistService.Object, mockAppContext, mockCacheManager.Object, mockLogger.Object);

            //Increment db records by 3
            for (int i = 0; i < 3; i++)
            {
                await binlistManager.IncrementCount(id);
            }

            var result = await binlistManager.GetCount();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BinCountModel));
            Assert.AreEqual(3, result.size);
            Assert.AreEqual(true, result.success);
            Assert.AreEqual(true, result.response.ContainsKey(id));

        }

        [TestMethod]
        public async Task GetBinDetailsTest()
        {
            var id = "1234";
            var binDetailsResult = new Bin() { id = id };

            var mockBinlistService = new Mock<IBinlistService>();
            var mockCacheManager = new Mock<ICacheManager>();
            var mockLogger = new Mock<ILogger<BinlistManager>>();

            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var mockAppContext = new AppDbContext(dbContextOptions);


            mockCacheManager.Setup(a => a.Get(It.IsAny<string>())).Returns("");
            mockCacheManager.Setup(a => a.Set(It.IsAny<string>(), It.IsAny<string>())).Returns("");

            mockBinlistService.Setup(a => a.GetBinDetails(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(binDetailsResult);

            var binlistManager = new BinlistManager(mockBinlistService.Object, mockAppContext, mockCacheManager.Object, mockLogger.Object);

            var result = await binlistManager.GetBinDetails(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.id);
            Assert.IsInstanceOfType(result, typeof(Bin));

        }


    }
}
